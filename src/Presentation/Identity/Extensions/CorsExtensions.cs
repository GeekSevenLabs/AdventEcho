namespace AdventEcho.Presentation.Identity.Extensions;

public static class CorsExtensions
{
    public static WebApplicationBuilder AddAdventEchoCorsPolicy(this WebApplicationBuilder builder, AdventEchoIdentityOptions options)
    {
        builder.Services.AddCors(opt =>
        {
            opt.AddPolicy(ServerConstants.CorsPolicy, builderPolicy =>
            {
                builderPolicy
                    .WithOrigins(options.Domains.Identity, options.Domains.Web, options.Domains.Api)
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });
        
        return builder;
    }
    
    public static WebApplication UseAdventEchoCorsPolicy(this WebApplication app)
    {
        app.UseCors(ServerConstants.CorsPolicy);
        
        return app;
    }
}