using AdventEcho.Identity.Application.Services.Cookies;
using AdventEcho.Identity.Application.Services.Tokens;
using AdventEcho.Identity.Application.Services.Users;
using AdventEcho.Identity.Application.Shared.Accounts.Login;
using AdventEcho.Kernel.Application.Services;
using Microsoft.Extensions.Logging;

namespace AdventEcho.Identity.Application.Accounts.Login;

internal class UserLoginHandler(
    IUserService userService, 
    ITokenService tokenService,
    ICookieService cookieService,
    ICacheService cacheService,
    ILogger<UserLoginHandler> logger) : IUserLoginHandler
{
    public async Task<Result<LoginResponse>> HandleAsync(LoginRequest request, CancellationToken cancellationToken = default)
    {
        var userResult = await userService.CheckPasswordSignInAsync(request.Email!, request.Password!, cancellationToken);
        if (userResult.IsFail(out var user, out var error))
        {
            logger.LogWarning("User login failed: {Error}", error.Message);
            return Result<LoginResponse>.Fail(error);
        }
        
        var tokensResult = await tokenService.GenerateTokensAsync(user);
        if (tokensResult.IsFail(out var tokens, out error))
        {
            logger.LogWarning("Token generation failed: {Error}", error.Message);
            return Result<LoginResponse>.Fail(error);
        }
        
        var expiration = tokens.RefreshToken.Expires - DateTime.UtcNow;
        
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

        var response = new LoginResponse { AccessToken = tokens.AccessToken, RefreshId = tokens.RefreshToken.Id };

        if (request.UseCookie is false) return response;
        
        var cookieSignInResult = await cookieService.SignIn(tokens);

        if (!cookieSignInResult.IsFail(out var cookieError)) return response;
        
        logger.LogWarning("Cookie sign-in failed: {Error}", cookieError.Message);
        return Result<LoginResponse>.Fail(cookieError);

    }
}