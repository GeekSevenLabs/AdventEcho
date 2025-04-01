using AdventEcho.Identity.Application.Shared.Accounts.Refresh;
using AdventEcho.Kernel.Application.Communication.Mediator;
using AdventEcho.Kernel.Server.Endpoints;
using AdventEcho.Kernel.Server.Extensions;
using AdventEcho.Kernel.Server.Validations;

namespace AdventEcho.Presentation.Identity.Endpoints.Accounts;

public class RefreshLoginAccountEndpoint : IEndpoint
{
    public static IEndpointRouteBuilder Map(IEndpointRouteBuilder endpoints)
    {
        endpoints
            .MapPost("/refresh-login", ExecuteAsync)
            .UseValidationFor<RefreshLoginAccountRequest>()
            .WithName("Refresh Login")
            .WithDescription("Refresh login to the system")
            .ProducesValidationProblem()
            .Produces<RefreshLoginAccountResponse>();
        
        return endpoints;
    }

    private static async Task<IResult> ExecuteAsync(RefreshLoginAccountRequest request, IMediatorHandler mediator)
    {
        return await mediator.SendCommandAsync(request).ProcessResult();
    }
}