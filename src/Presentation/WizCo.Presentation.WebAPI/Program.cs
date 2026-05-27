using System.Text.Json.Serialization;
using WizCo.Infra.IoC;
using WizCo.Presentation.WebAPI.Configuration;
using WizCo.Presentation.WebAPI.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters
            .Add(new JsonStringEnumConverter());
    });

builder.Services.AddApiConfig();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddInfrastructureJWT(builder.Configuration);
builder.Services.AddRateLimiting();
builder.Services.AddInfrastructureSwagger();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.UseSwaggerConfiguration();

app.UseRouting();
app.ApplyMigrations();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ExceptionMiddleware>();
app.UseRateLimiter();
app.MapControllers();

app.Run();
