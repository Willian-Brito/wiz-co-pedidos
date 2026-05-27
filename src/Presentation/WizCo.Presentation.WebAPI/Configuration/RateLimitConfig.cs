using System.Threading.RateLimiting;

namespace WizCo.Presentation.WebAPI.Configuration;

public static class RateLimitConfig
{
    public static IServiceCollection AddRateLimiting(this IServiceCollection services)
    {
        services.AddRateLimiter(options =>
        {
            options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

            #region AUTH POLICY
            options.AddPolicy("Auth", httpContext =>
            {
                var ip = httpContext.Connection.RemoteIpAddress?.ToString() ?? "anonymous";

                return RateLimitPartition
                    .GetSlidingWindowLimiter(
                        ip,
                        _ => new SlidingWindowRateLimiterOptions
                        {
                            PermitLimit = 5,
                            Window = TimeSpan.FromMinutes(1),
                            SegmentsPerWindow = 6,
                            QueueLimit = 0,
                            AutoReplenishment = true
                        }
                    );
            });
            #endregion

            #region API POLICY
            
            options.AddPolicy("Api", httpContext =>
            {
                var user = httpContext.User.Identity?.Name;
                var ip = httpContext.Connection.RemoteIpAddress?.ToString();
                var partitionKey = user ?? ip ?? "anonymous";

                return RateLimitPartition
                    .GetSlidingWindowLimiter(
                        partitionKey,
                        _ => new SlidingWindowRateLimiterOptions
                        {
                            PermitLimit = 200,
                            Window = TimeSpan.FromMinutes(1),
                            SegmentsPerWindow = 6,
                            QueueLimit = 0,
                            AutoReplenishment = true
                        }
                    );
            });
            #endregion
            
            #region CUSTOM RESPONSE
            
            options.OnRejected = async (context, token) =>
            {
                var httpContext = context.HttpContext;
                var retryAfter =
                    context.Lease.TryGetMetadata(MetadataName.RetryAfter, out var retryAfterValue)
                        ? retryAfterValue.TotalSeconds
                        : 60;

                httpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                httpContext.Response.ContentType = "application/json";
                httpContext.Response.Headers.RetryAfter = retryAfter.ToString();

                var logger = httpContext.RequestServices.GetRequiredService<ILogger<Program>>();

                logger.LogWarning(
                    "Rate limit excedido para IP/User: {Identifier}",
                    httpContext.User.Identity?.Name ??
                    httpContext.Connection.RemoteIpAddress?.ToString()
                );

                await httpContext.Response.WriteAsJsonAsync(
                    new
                    {
                        success = false,
                        error = "rate_limit_exceeded",
                        message = "Você excedeu o limite de requisições. Tente novamente mais tarde.",
                        retryAfter = retryAfter
                    },
                    cancellationToken: token
                );
            };
            #endregion
        });
        
        return services;
    }
}