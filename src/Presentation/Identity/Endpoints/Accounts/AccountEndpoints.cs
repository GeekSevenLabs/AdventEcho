namespace AdventEcho.Presentation.Identity.Endpoints.Accounts;

public static class AccountEndpoints
{
    public static IEndpointRouteBuilder MapAccountEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints
            .MapEndpoint<LoginAccountEndpoint>()
            .MapEndpoint<ConfirmEmailAccountEndpoint>()
            .MapEndpoint<RefreshLoginAccountEndpoint>()
            .MapEndpoint<RegisterAccountEndpoint>();

        return endpoints;
    }
}