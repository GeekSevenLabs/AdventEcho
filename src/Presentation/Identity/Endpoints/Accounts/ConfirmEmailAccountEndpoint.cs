using AdventEcho.Identity.Application.Shared.Accounts.ConfirmEmail;
using AdventEcho.Kernel.Application.Communication.Mediator;
using AdventEcho.Kernel.Server.Endpoints;
using AdventEcho.Kernel.Server.Extensions;
using AdventEcho.Kernel.Server.Validations;

namespace AdventEcho.Presentation.Identity.Endpoints.Accounts;

public class ConfirmEmailAccountEndpoint : IEndpoint
{
    public static IEndpointRouteBuilder Map(IEndpointRouteBuilder endpoints)
    {
        endpoints
            .MapPost("/confirm-email", ExecuteAsync)
            .UseValidationFor<ConfirmEmailAccountRequest>()
            .WithName("Confirm Email")
            .WithDescription("Confirm your email")
            .ProducesValidationProblem();
        
        return endpoints;
    }

    private static async Task<IResult> ExecuteAsync(ConfirmEmailAccountRequest request, IMediatorHandler mediator)
    {
        return await mediator.SendCommandAsync(request).ProcessResult();
    }
}