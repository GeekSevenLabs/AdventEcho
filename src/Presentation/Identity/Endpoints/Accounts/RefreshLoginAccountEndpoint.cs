using AdventEcho.Identity.Application.Shared.Accounts.Refresh;

namespace AdventEcho.Presentation.Identity.Endpoints.Accounts;

using static AdventEcho.Identity.Application.Shared.Constants.Routes.Account;

public sealed class RefreshLoginAccountEndpoint : IEndpoint
{
    public static IEndpointRouteBuilder Map(IEndpointRouteBuilder endpoints)
    {
        endpoints
            .MapPost(Refresh.Endpoint, ExecuteAsync)
            .WithDefinition(Refresh)
            .UseValidationFor<RefreshLoginAccountRequest>()
            .ProducesCommonResponse<RefreshLoginAccountResponse>()
            .AllowAnonymous();
        
        return endpoints;
    }

    private static async Task<IResult> ExecuteAsync(RefreshLoginAccountRequest request, IMediatorHandler mediator)
    {
        return await mediator.SendCommandAsync(request).ProcessResult();
    }
}