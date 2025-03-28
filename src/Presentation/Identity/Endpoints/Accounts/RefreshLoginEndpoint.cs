using AdventEcho.Identity.Application.Accounts.Refresh;

using AdventEcho.Kernel.Messages;

namespace AdventEcho.Presentation.Identity.Endpoints.Accounts;

public class RefreshLoginEndpoint : IEndpoint
{
    public static IEndpointRouteBuilder Map(IEndpointRouteBuilder endpoints)
    {
        endpoints
            .MapPost("/refresh-login", RefreshLoginAsync)
            .WithName("RefreshLogin")
            .WithSummary("Refresh Login")
            .WithDescription("Refresh the login of a user with the provided refresh token. After successful refresh, the user will receive a new token to authenticate subsequent requests in this API or other APIs.")
            .AddCommonProduces()
            .RequireAuthorization(policyBuilder =>
            {
                policyBuilder.RequireClaim(AdventEchoClaims.RefreshToken);
            });
        
        return endpoints;
    }
    
    private static async Task<IResult> RefreshLoginAsync(HttpContext context, IRefreshLoginHandler handler)
    {
        if(context.User.GetUserId().IsFail(out var userId, out var error)) 
            return Result.Fail(error).ProcessResult();
        
        var request = new RefreshLoginRequest { UserId = userId };
        return await handler.HandleAsync(request).ProcessResult();
    }
}