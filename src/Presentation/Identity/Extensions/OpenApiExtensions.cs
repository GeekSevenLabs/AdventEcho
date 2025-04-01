using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

namespace AdventEcho.Presentation.Identity.Extensions;

public static class OpenApiExtensions
{
    public static WebApplicationBuilder AddAdventEchoIdentityOpenApiServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddOpenApi(options =>
        {
            options.AddDocumentTransformer((apiDocument, _, _) =>
            {
                apiDocument.Info.Title = "AdventEcho Identity API";
                apiDocument.Info.Description = "API for User identity management";
                apiDocument.Info.Version = "v1";
                
                apiDocument.Servers = [];
        
                apiDocument.Components ??= new OpenApiComponents();
                apiDocument.Components.SecuritySchemes = new Dictionary<string, OpenApiSecurityScheme>
                {
                    ["Authorization"] = new ()
                    {
                        Name = "Authorization",
                        Type = SecuritySchemeType.Http,
                        Scheme = JwtBearerDefaults.AuthenticationScheme,
                        BearerFormat = "JWT",
                        In = ParameterLocation.Header,
                        Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\""
                    }
                };
        
                return Task.CompletedTask;
            });
        });
        
        builder.Services.AddEndpointsApiExplorer();

        return builder;
    }
}