using System.Security.Claims;
using AdventEcho.Presentation.Identity.Endpoints.Accounts;

namespace AdventEcho.Presentation.Identity.Endpoints;

public static class VersionOneEndpoints
{
    public static IEndpointRouteBuilder MapAdventEchoIdentityEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints
            .MapGroup("/v1")
            .WithTags("Advent Echo Identity API");
        
        group
            .MapGet("/health", () => Results.Ok("Healthy"))
            .WithName("Health Check")
            .WithDescription("Check the health of the API")
            .Produces<string>();
        
        group
            .MapGet("/health-auth", (ClaimsPrincipal principal) => Results.Ok($"Healthy - User: {principal.Identity?.Name}"))
            .WithName("Health Check Auth")
            .WithDescription("Check the health of the API")
            .Produces<string>()
            .RequireAuthorization();

        group.MapAccountEndpoints();

        return endpoints;
    }
}