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
    
    private class HealthCheckResponse
    {
        public bool Success { get; set; } = true;
        public DateTimeOffset Timestamp { get; set; } = DateTimeOffset.UtcNow;
    }
}