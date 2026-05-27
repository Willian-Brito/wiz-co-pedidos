using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WizCo.Infra.Data.Context;

namespace WizCo.Tests.IntegrationTest.Configurations;

public class CustomWebApplicationFactory : WebApplicationFactory<Presentation.WebAPI.Program>
{
    private SqliteConnection _connection = null!;
        
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");
            
        builder.ConfigureAppConfiguration((context, config) =>
        {
            var settings = new Dictionary<string, string?>
            {
                ["JwtSettings:Secret"] = "%abra#cadabra$sim@salabim*2023|%abra#cadabra$sim@salabim*2023|123456789",
                ["JwtSettings:Issuer"] = "Test",
                ["JwtSettings:Audience"] = "Test"
            };
        
            config.AddInMemoryCollection(settings);
        });
        
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                     typeof(DbContextOptions<PedidoDbContext>)
            );

            if (descriptor != null)
                services.Remove(descriptor);
            
            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();
            
            services.AddDbContext<PedidoDbContext>(options =>
            {
                options.UseSqlite(_connection);
            });
            
            // Auth Fake
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = "Test";
                    options.DefaultChallengeScheme = "Test";
                })
                .AddScheme<TestAuthenticationSchemeOptions, TestAuthenticationHandler>("Test", options => { });
            
            services.PostConfigureAll<AuthenticationOptions>(options =>
            {
                options.DefaultAuthenticateScheme = "Test";
                options.DefaultChallengeScheme = "Test";
                options.DefaultScheme = "Test";
            });
            
            // var provider = services.BuildServiceProvider();
            // using var scope = provider.CreateScope();
            // var db = scope.ServiceProvider.GetRequiredService<PedidoDbContext>();
            //
            // db.Database.EnsureCreated();
            // db.Database.EnsureCreated();
        });
    }
}