using AdventEcho.Kernel.Server.Extensions;

namespace AdventEcho.Presentation.Identity.Endpoints.Accounts;

public static class AccountEndpoints
{
    public static IEndpointRouteBuilder MapAccountEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints
            .MapGroup("/accounts")
            .WithDescription("Account endpoints")
            .WithTags("Account");

        group
            .MapEndpoint<LoginAccountEndpoint>()
            .MapEndpoint<ConfirmEmailAccountEndpoint>()
            .MapEndpoint<RefreshLoginAccountEndpoint>()
            .MapEndpoint<RegisterAccountEndpoint>();

        return endpoints;
    }
}