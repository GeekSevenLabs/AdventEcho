using AdventEcho.Identity.Application.Services.Caches;
using AdventEcho.Identity.Application.Services.Cookies;
using AdventEcho.Identity.Application.Services.Tokens;
using AdventEcho.Identity.Application.Services.Users;
using AdventEcho.Identity.Application.Shared.Accounts.Login;
using AdventEcho.Kernel.Application.Handlers;
using AdventEcho.Kernel.Application.Shared.Messages.Results;
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
        if (userResult.IsFail(out var user, out var error))
        {
            logger.LogWarning("User login failed: {Error}", error.Message);
            return EchoResults<LoginAccountResponse>.Fail(error);
        }
        
        var tokensResult = await tokenService.GenerateTokensAsync(user);
        if (tokensResult.IsFail(out var tokens, out error))
        {
            logger.LogWarning("Token generation failed: {Error}", error.Message);
            return EchoResults<LoginAccountResponse>.Fail(error);
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

        if (request.UseCookie is false) return EchoResults<LoginAccountResponse>.Success(response);
        
        var setTokensResult = await cookieService.SetTokensIntoCookieAsync(tokens);

        if (!setTokensResult.IsFail(out var cookieError)) return EchoResults<LoginAccountResponse>.Success(response);
        
        logger.LogWarning("Cookie storage failed: {Error}", cookieError.Message);
        return Result<LoginAccountResponse>.Fail(cookieError);
    }
}