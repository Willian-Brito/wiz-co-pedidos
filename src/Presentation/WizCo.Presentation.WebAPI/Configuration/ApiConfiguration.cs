namespace WizCo.Presentation.WebAPI.Configuration;

public static class ApiConfiguration
{
    public static IServiceCollection AddApiConfig(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddRouting(options =>
        {
            options.LowercaseUrls = true;
        });

        return services;
    }
}