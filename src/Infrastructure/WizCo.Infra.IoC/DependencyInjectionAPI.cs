using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WizCo.Core.Application.Communication;
using WizCo.Core.Application.Mappings;
using WizCo.Core.Application.Results;
using WizCo.Core.Application.UseCases.Pedidos.Commands.CancelarPedido;
using WizCo.Core.Application.UseCases.Pedidos.Commands.CriarPedido;
using WizCo.Core.Application.UseCases.Pedidos.DTOs;
using WizCo.Core.Application.UseCases.Pedidos.Queries;
using WizCo.Core.Domain.Interfaces;
using WizCo.Infra.Data.Context;
using WizCo.Infra.Data.Identity;
using WizCo.Infra.Data.Repositories;

namespace WizCo.Infra.IoC;

public static class DependencyInjectionAPI
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Database
        services.AddDbContext<PedidoDbContext>(options =>
        {
            options.UseSqlite(
                configuration.GetConnectionString("DefaultConnection"));
        });
        
        services.AddScoped<IPedidoRepository, PedidoRepository>();
        services.AddScoped<IPedidoQueries, PedidoQueries>();
        services.AddScoped<PedidoDbContext>();
        services.AddScoped<IUnitOfWork>(provider => provider.GetRequiredService<PedidoDbContext>());
        
        // Application
        services.AddScoped<IMessageBus, MessageBus>();
        services.AddScoped<IRequestHandler<CriarPedidoCommand, Result<PedidoDTO>>, CriarPedidoCommandHandler>();
        services.AddScoped<IRequestHandler<CancelarPedidoCommand, Result>, CancelarPedidoCommandHandler>();
        
        // Identity
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddScoped<IAuthenticateService, AuthenticateService>();
        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<PedidoDbContext>()
            .AddDefaultTokenProviders();

        services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireNonAlphanumeric = false;

            options.Password.RequiredLength = 8;
            options.Password.RequiredUniqueChars = 0;
        });
        
        // Auto Mapper
        services.AddAutoMapper(typeof(PedidoMappingProfile));
        
        // MediatR
        var handlers = AppDomain.CurrentDomain.Load("WizCo.Core.Application");
        services.AddMediatR(config => config.RegisterServicesFromAssemblies(handlers));
        
        return services;
    }
    
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using (var serviceScope = app.ApplicationServices.CreateScope())
        {            
            var context = serviceScope.ServiceProvider.GetRequiredService<PedidoDbContext>();   
            context.Database.Migrate();
        }
    }
}