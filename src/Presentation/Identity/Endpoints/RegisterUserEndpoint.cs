using AdventEcho.Identity.Application.Shared;
using AdventEcho.Identity.Application.Shared.Register;
using AdventEcho.Kernel.Server.Endpoints;
using AdventEcho.Kernel.Server.Extensions;
using AdventEcho.Kernel.Server.Validations;

namespace AdventEcho.Presentation.Identity.Endpoints;

public class RegisterUserEndpoint : IEndpoint
{
    public static IEndpointRouteBuilder Map(IEndpointRouteBuilder endpoints)
    {
        endpoints
            .MapPost("/", RegisterUserAsync)
            .UseValidationFor<RegisterUserRequest>()
            .WithName("RegisterUser")
            .WithDescription("Register a new user.")
            .WithSummary("Register a new user.");
        
        
        return endpoints;
    }
    
    private static async Task<IResult> RegisterUserAsync(RegisterUserRequest request, IAccountService accountService)
    {
        return await accountService.RegisterAsync(request).ProcessResult();
    }
}