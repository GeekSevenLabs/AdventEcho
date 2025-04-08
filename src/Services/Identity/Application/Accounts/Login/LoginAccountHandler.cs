using AdventEcho.Identity.Application.Services.Caches;
using AdventEcho.Identity.Application.Services.Cookies;
using AdventEcho.Identity.Application.Services.Tokens;
using AdventEcho.Identity.Application.Services.Users;
using AdventEcho.Identity.Application.Shared.Accounts.Login;
using AdventEcho.Kernel.Application.Handlers;
using Microsoft.Extensions.Logging;

namespace AdventEcho.Identity.Application.Accounts.Login;

public class LoginAccountHandler(
    IUserService userService,
    IBearerTokenService tokenService,
    ICookieService cookieService,
    ICacheService cacheService,
    ILogger<LoginAccountHandler> logger)
    : ICommandHandler<LoginAccountRequest, LoginAccountResponse>
{
    public async Task<Result<LoginAccountResponse>> Handle(LoginAccountRequest request, CancellationToken cancellationToken)
    {
        var userResult = await userService.CheckPasswordSignInAsync(request.Email!, request.Password!, cancellationToken);
        if (userResult.IsFail(out var user, out var errors))
        {
            logger.LogWarning("User login failed");
            return errors;
        }
        
        var tokensResult = await tokenService.GenerateTokensAsync(user);
        if (tokensResult.IsFail(out var tokens, out errors))
        {
            logger.LogWarning("Token generation failed");
            return errors;
        }
        
        var expiration = tokens.RefreshToken.Expiration - DateTimeOffset.UtcNow;
        
        await cacheService.SetStringAsync(
            tokens.RefreshToken.ToCacheKey(), 
            tokens.RefreshToken.ToJson(), 
            expiration, 
            cancellationToken
        );

        await cacheService.SetAddAsync(
            tokens.RefreshToken.ToUserCacheKey(),
            tokens.RefreshToken.ToCacheKey(),
            cancellationToken
        );

        var response = new LoginAccountResponse
        {
            AccessToken = tokens.AccessToken, 
            RefreshToken = tokens.RefreshToken.Id.ToString("N")
        };

        if (request.UseCookie is false) return response;
        
        var setTokensResult = await cookieService.SetTokensIntoCookieAsync(tokens);

        if (!setTokensResult.IsFail(out var cookieError)) return response;
        
        logger.LogWarning("Cookie storage failed");
        return cookieError;
    }
}