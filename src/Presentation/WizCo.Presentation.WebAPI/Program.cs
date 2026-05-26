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
builder.Services.AddInfrastructureSwagger();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
    app.UseSwaggerConfiguration();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
