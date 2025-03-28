namespace AdventEcho.Presentation.Identity.Endpoints.Accounts;

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

        group
            .MapEndpoint<ConfirmUserEmailEndpoint>()
            .MapEndpoint<LoginEndpoint>()
            .MapEndpoint<RefreshLoginEndpoint>()
            .MapEndpoint<RegisterUserEndpoint>();
    }
}