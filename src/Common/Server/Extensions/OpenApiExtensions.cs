using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.Extensions.DependencyInjection;
using Scalar.AspNetCore;

namespace AdventEcho.Kernel.Server.Extensions;

public static class OpenApiExtensions
{
    public static void AddAdventEchoOpenApi(this IServiceCollection services, Action<OpenApiOptions> options)
    {
        services.AddEndpointsApiExplorer();
        services.AddOpenApi(options);
    }
    
    public static void MapAdventEchoOpenApi(this WebApplication app)
    {
        app.MapOpenApi();
        app.MapScalarApiReference();
    }
}