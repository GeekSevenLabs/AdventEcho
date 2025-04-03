using AdventEcho.Kernel.Infrastructure.Options;
using Menso.Tools.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Resend;

namespace AdventEcho.Kernel.Server.Extensions;

public static class OptionsExtensions
{
    public static AdventEchoIdentityOptions AddAdventEchoIdentityOptions(this WebApplicationBuilder builder)
    {
        var section =  builder.Configuration.GetRequiredSection(AdventEchoIdentityOptions.SectionName);
        
        var config = section.Get<AdventEchoIdentityOptions>();
        
        Throw.When.Null(config, "Identity configuration is missing");
        Throw.When.Null(config.Cookie, "Identity configuration is missing");
        Throw.When.Null(config.Domains, "Identity configuration is missing");
        Throw.When.Null(config.Bearer, "Identity configuration is missing");
        
        builder.Services.AddOptions();
        builder.Services.Configure<AdventEchoIdentityOptions>(section);
        builder.Services.Configure<ResendClientOptions>(options =>
        {
            options.ApiToken = builder.Configuration["ResendKey"]!;
        });

        return config;
    }
}