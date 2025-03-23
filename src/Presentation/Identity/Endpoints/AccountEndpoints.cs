using AdventEcho.Kernel.Server.Extensions;

namespace AdventEcho.Presentation.Identity.Endpoints;

public static class AccountEndpoints
{
    public static void MapAccountEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints
            .MapGroup("account")
            .WithName("Account")
            .WithSummary("Account endpoints.")
            .WithDescription("Endpoints for managing accounts.")
            .WithTags("Account");
            
        group.MapEndpoint<RegisterUserEndpoint>();
    }
}