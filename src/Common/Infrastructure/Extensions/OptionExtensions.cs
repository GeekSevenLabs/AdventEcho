using AdventEcho.Kernel.Infrastructure.Options;
using Menso.Tools.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AdventEcho.Kernel.Infrastructure.Extensions;

internal static class OptionExtensions
{
    public static AdventEchoIdentityForClientOption AddAdventEchoIdentityForClientOptions(this WebApplicationBuilder builder)
    {
        var section =  builder.Configuration.GetRequiredSection(AdventEchoIdentityForClientOption.SectionName);
        var config = section.Get<AdventEchoIdentityForClientOption>();
        Throw.When.Null(config, "Identity configuration is missing");
        Throw.When.Null(config.Cookie, "Identity configuration is missing");
        Throw.When.Null(config.Domains, "Identity configuration is missing");
        Throw.When.Null(config.Jwt, "Identity configuration is missing");
        
        builder.Services.AddOptions();
        builder.Services.Configure<AdventEchoIdentityForClientOption>(builder.Configuration.GetSection(AdventEchoIdentityForClientOption.SectionName));

        return config;
    }
}