using AdventEcho.Identity.Application.Shared.Accounts.Login;
using AdventEcho.Kernel.Application.Communication.Mediator;
using AdventEcho.Kernel.Server.Endpoints;
using AdventEcho.Kernel.Server.Extensions;
using AdventEcho.Kernel.Server.Validations;

namespace AdventEcho.Presentation.Identity.Endpoints.Accounts;

public class LoginAccountEndpoint : IEndpoint
{
    public static IEndpointRouteBuilder Map(IEndpointRouteBuilder endpoints)
    {
        endpoints
            .MapPost("/login", ExecuteAsync)
            .UseValidationFor<LoginAccountRequest>()
            .WithName("Login")
            .WithDescription("Login to the system")
            .ProducesValidationProblem()
            .Produces<LoginAccountResponse>();
        
        return endpoints;
    }

    private static async Task<IResult> ExecuteAsync(LoginAccountRequest request, IMediatorHandler mediator)
    {
        return await mediator.SendCommandAsync(request).ProcessResult();
    }
}