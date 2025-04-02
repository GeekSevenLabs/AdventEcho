using AdventEcho.Identity.Application.Shared;
using AdventEcho.Identity.Application.Shared.Accounts.Login;
using AdventEcho.Kernel.Application.Communication.Mediator;
using AdventEcho.Kernel.Server.Endpoints;
using AdventEcho.Kernel.Server.Extensions;
using AdventEcho.Kernel.Server.Validations;

namespace AdventEcho.Presentation.Identity.Endpoints.Accounts;

using static Constants.Routes.Account;

public class LoginAccountEndpoint : IEndpoint
{
    public static IEndpointRouteBuilder Map(IEndpointRouteBuilder endpoints)
    {
        endpoints
            .MapPost(Login.Endpoint, ExecuteAsync)
            .WithDefinition(Login)
            .UseValidationFor<LoginAccountRequest>()
            .ProducesCommonResponse<LoginAccountResponse>()
            .AllowAnonymous();
        
        return endpoints;
    }

    private static async Task<IResult> ExecuteAsync(LoginAccountRequest request, IMediatorHandler mediator)
    {
        return await mediator.SendCommandAsync(request).ProcessResult();
    }
}