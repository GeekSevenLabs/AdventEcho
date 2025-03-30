using System.ComponentModel;
using AdventEcho.Identity.Application.Accounts.Refresh;
using AdventEcho.Identity.Infrastructure.Options;
using AdventEcho.Kernel.Exceptions;
using AdventEcho.Kernel.Messages;
using Microsoft.Extensions.Options;

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
            .AllowAnonymous();
        
        return endpoints;
    }
    
    private static async Task<IResult> RefreshLoginAsync(
        [AsParameters] Request requestTemp, 
        HttpContext context,
        IOptions<AdventEchoIdentityOption> options,
        IRefreshLoginHandler handler,
        ILogger<IRefreshLoginHandler> logger)
    {
        var requestResult = GetRefreshId(context, requestTemp.RefreshId, options.Value);
        if (requestResult.IsFail(out var request, out _))
        {
            logger.LogWarning("Failed to get refresh id from cookie or query string");
            return requestResult.ProcessResult();
        }
        
        return await handler.HandleAsync(request).ProcessResult();
    }
    
    private static Result<RefreshLoginRequest> GetRefreshId(HttpContext context, string? refreshIdString, AdventEchoIdentityOption configs)
    {
        if (!string.IsNullOrWhiteSpace(refreshIdString))
        {
            if (!Guid.TryParse(refreshIdString, out var refreshId))
            {
                return UnauthorizedException.New;
            }
            
            return new RefreshLoginRequest{ RefreshId = refreshId, UseCookie = false };
        }
        
        // Get refresh id from cookie
        var cookie = context.Request.Cookies[configs.Cookie.RefreshIdName];
        if (string.IsNullOrWhiteSpace(cookie)) return UnauthorizedException.New;
        
        if (!Guid.TryParse(cookie, out var refreshIdValue)) return UnauthorizedException.New;
        
        return new RefreshLoginRequest { RefreshId = refreshIdValue, UseCookie = true };
    }
    
    private class Request
    {
        [Description("Use only when the cookie scheme is not used")]
        public string? RefreshId { get; set; }
    }
}