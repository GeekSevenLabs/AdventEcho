using AdventEcho.Identity.Application.Shared.Accounts.Register;

namespace AdventEcho.Presentation.Identity.Endpoints.Accounts;

using static AdventEcho.Identity.Application.Shared.Constants.Routes.Account;

public sealed class RegisterAccountEndpoint : IEndpoint
{
    public static IEndpointRouteBuilder Map(IEndpointRouteBuilder endpoints)
    {
        endpoints
            .MapPost(Register.Endpoint, ExecuteAsync)
            .WithDefinition(Register)
            .UseValidationFor<RegisterAccountRequest>()
            .ProducesCommonResponse()
            .AllowAnonymous();
        
        return endpoints;
    }

    private static async Task<IResult> ExecuteAsync(RegisterAccountRequest request, IMediatorHandler mediator)
    {
        return await mediator.SendCommandAsync(request).ProcessResult();
    }
}