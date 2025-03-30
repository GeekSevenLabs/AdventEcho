using System.Text.Json.Serialization;
using AdventEcho.Kernel.Infrastructure.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace AdventEcho.Kernel.Server.Endpoints;

public static class HealthCheckEndpoint
{
    public static void MapHealthCheck(this IEndpointRouteBuilder endpoints)
    {
        endpoints
            .MapGet("/health", () => TypedResults.Ok(new HealthCheckResponse()))
            .WithName("HealthCheck")
            .WithDisplayName("Health Check")
            .WithSummary("Check to see if the advent echo identity service is running.")
            .WithTags("Health Check")
            .AllowAnonymous();
    }
    
    public static void MapHealthCheckAuthorized(this IEndpointRouteBuilder endpoints)
    {
        endpoints
            .MapGet("/health-auth", (HttpContext context) => TypedResults.Ok(new HealthCheckResponse(context.User.Identity?.Name)))
            .WithName("HealthCheckAuthorized")
            .WithDisplayName("Health Check Authorized")
            .WithSummary("Check to see if the advent echo identity service is running with authorization.")
            .WithTags("Health Check")
            .RequireAuthorization(option =>
            {
                option.AddAuthenticationSchemes(AdventEchoIdentityCookieDefaults.AuthenticationScheme);
                option.RequireAuthenticatedUser();
            });
    }
    
    private class HealthCheckResponse(string? userName = null)
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? UserName { get; set; } = userName;
        public bool Success { get; set; } = true;
        public DateTimeOffset Timestamp { get; set; } = DateTimeOffset.UtcNow;
    }
}