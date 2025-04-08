using AdventEcho.Identity.Application.Shared.Accounts.ConfirmEmail;

namespace AdventEcho.Presentation.Identity.Endpoints.Accounts;

using static AdventEcho.Identity.Application.Shared.Constants.Routes.Account;

public class ConfirmEmailAccountEndpoint : IEndpoint
{
    public static IEndpointRouteBuilder Map(IEndpointRouteBuilder endpoints)
    {
        endpoints
            .MapPost(ConfirmEmail.Endpoint, ExecuteAsync)
            .WithDefinition(ConfirmEmail)
            .UseValidationFor<ConfirmEmailAccountRequest>()
            .ProducesCommonResponse()
            .AllowAnonymous();
        
        return endpoints;
    }

    private static async Task<IResult> ExecuteAsync(ConfirmEmailAccountRequest request, IMediatorHandler mediator)
    {
        return await mediator.SendCommandAsync(request).ProcessResult();
    }
}