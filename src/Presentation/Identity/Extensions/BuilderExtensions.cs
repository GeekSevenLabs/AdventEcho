using AdventEcho.Identity.Application;
using AdventEcho.Identity.Infrastructure;
using AdventEcho.Kernel.Application.Communication.Mediator;
using AdventEcho.Kernel.Server.Extensions;
using AdventEcho.Presentation.Identity.Endpoints;
using Scalar.AspNetCore;

namespace AdventEcho.Presentation.Identity.Extensions;

public static class BuilderExtensions
{
    public static WebApplicationBuilder AddAdventEchoIdentityServices(this WebApplicationBuilder builder)
    {
        var options = builder.AddAdventEchoIdentityOptions();

        builder.Services.AddScoped<IMediatorHandler, MediatorHandler>();
        builder.Services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblyContaining<Program>();
        });
        
        builder
            .AddAdventEchoCorsPolicy(options)    
            .AddAdventEchoIdentityInfrastructureServices()
            .AddAdventEchoIdentityOpenApiServices()
            .AddAdventEchoIdentitySecurityServices(options);

        builder.Services.AddAdventEchoIdentityHandlersServices();
        
        return builder;
    }

    public static async Task<WebApplication> MapAdventEchoIdentity(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            await app.ApplyMigrationsAsync();
            app.MapOpenApi();
            app.MapScalarApiReference();
        }

        app.UseAdventEchoCorsPolicy();
        
        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapAdventEchoIdentityEndpoints();

        return app;
    }
}