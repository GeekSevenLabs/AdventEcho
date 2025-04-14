using System.Security.Claims;
using AdventEcho.Presentation.Identity.Endpoints.Accounts;

namespace AdventEcho.Presentation.Identity.Endpoints;

public static class VersionOneEndpoints
{
    public static IEndpointRouteBuilder MapAdventEchoIdentityEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints
            .MapGet("/health", () => Results.Ok("Healthy"))
            .WithName("Health Check")
            .WithDescription("Check the health of the API")
            .Produces<string>();

        endpoints
            .MapGet("/health-auth", (ClaimsPrincipal principal) => Results.Ok($"Healthy - User: {principal.Identity?.Name}"))
            .WithName("Health Check Auth")
            .WithDescription("Check the health of the API")
            .Produces<string>()
            .RequireAuthorization();

        endpoints.MapAccountEndpoints();

        return endpoints;
    }
}