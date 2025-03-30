using AdventEcho.Identity.Infrastructure.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace AdventEcho.Identity.IoC.Services;

public static class CorsExtensions
{
    public static void AddAdventEchoCors(this IServiceCollection services, AdventEchoIdentityOption configs)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(AdventEchoIdentityOption.Cors.PolicyName, policy =>
            {
                policy.WithOrigins(configs.Domains.Identity, configs.Domains.Web)
                    .AllowCredentials()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });
    }
    
    public static void UseAdventEchoCors(this IApplicationBuilder app)
    {
        app.UseCors(AdventEchoIdentityOption.Cors.PolicyName);
    }
}