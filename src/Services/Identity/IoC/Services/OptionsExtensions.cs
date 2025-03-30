using AdventEcho.Identity.Infrastructure.Options;
using Menso.Tools.Exceptions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Resend;

namespace AdventEcho.Identity.IoC.Services;

public static class OptionsExtensions
{
    public static AdventEchoIdentityOption AddAdventEchoIdentityOptions(this IServiceCollection services, IConfiguration configuration)
    {
        var section =  configuration.GetRequiredSection(AdventEchoIdentityOption.SectionName);
        var config = section.Get<AdventEchoIdentityOption>();
        Throw.When.Null(config, "Identity configuration is missing");
        Throw.When.Null(config.Cookie, "Identity configuration is missing");
        Throw.When.Null(config.Domains, "Identity configuration is missing");
        Throw.When.Null(config.Jwt, "Identity configuration is missing");
        
        services.AddOptions();
        services.Configure<AdventEchoIdentityOption>(configuration.GetSection(AdventEchoIdentityOption.SectionName));
        services.Configure<ResendClientOptions>(options => { options.ApiToken = configuration["ResendKey"]!; });

        return config;
    }
}