using AdventEcho.Identity.Application.Accounts.Login;
using AdventEcho.Identity.Application.Shared.Accounts.Login;

namespace AdventEcho.Presentation.Identity.Endpoints.Accounts;

public class LoginEndpoint : IEndpoint
{
    public static IEndpointRouteBuilder Map(IEndpointRouteBuilder endpoints)
    {
        endpoints
            .MapPost("/login", LoginAsync)
            .UseValidationFor<LoginRequest>()
            .WithName("Login")
            .WithSummary("Login")
            .WithDescription("Login a user with the provided credentials. After successful login, the user will receive a token to authenticate subsequent requests in this API or other APIs.")
            .AddCommonProduces()
            .AllowAnonymous();
        
        return endpoints;
    }
    
    private static async Task<IResult> LoginAsync(LoginRequest request, IUserLoginHandler handler)
    {
        return await handler.HandleAsync(request).ProcessResult();
    }
}