using AdventEcho.Identity.Application.Services.Cookies;
using AdventEcho.Identity.Application.Services.Tokens;
using AdventEcho.Identity.Application.Services.Users;
using AdventEcho.Identity.Application.Shared.Accounts.Refresh;
using AdventEcho.Kernel.Application.Services;
using AdventEcho.Kernel.Exceptions;
using Microsoft.Extensions.Logging;

namespace AdventEcho.Identity.Application.Accounts.Refresh;

internal class RefreshLoginHandler(
    ITokenService tokenService,
    ICacheService cacheService,
    ICookieService cookieService,
    IUserService userService,
    ILogger<IRefreshLoginHandler> logger) : IRefreshLoginHandler
{
    public async Task<Result<RefreshLoginResponse>> HandleAsync(RefreshLoginRequest request, CancellationToken cancellationToken = default)
    {
        var refreshTokenJson = await cacheService.GetStringAsync(RefreshToken.ToCacheKey(request.RefreshId), cancellationToken);
        if (string.IsNullOrEmpty(refreshTokenJson))
        {
            logger.LogWarning("Refresh token not found in cache");
            return UnauthorizedException.New;
        }
        
        var refreshTokenResult = RefreshToken.FromJson(refreshTokenJson);
        if (refreshTokenResult.IsFail(out var refreshToken, out var refreshTokenError)) return refreshTokenError;
        
        if (refreshToken.IsExpired())
        {
            logger.LogWarning("Refresh token expired or invalid for user {UserId}", refreshToken.UserId);
            await cacheService.RemoveAsync(refreshToken.ToCacheKey(), cancellationToken);
            return UnauthorizedException.New;
        }
        
        var currentUserResult = await userService.GetUserIdAsync(refreshToken.UserId, cancellationToken);
        if (currentUserResult.IsFail(out var user, out var currentUserError))
        {
            logger.LogWarning("User not found for refresh token {RefreshId}: {Message}", request.RefreshId, currentUserError.Message);
            await cacheService.RemoveAsync(refreshToken.ToCacheKey(), cancellationToken);
            return currentUserError;
        }
        
        var tokensResult = await tokenService.GenerateTokensAsync(user);
        if (tokensResult.IsFail(out var tokens, out var tokensError))
        {
            logger.LogWarning("Failed to generate tokens for user {UserId}: {Message}", user.Id, tokensError.Message);
            return tokensError;
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

        await cacheService.RemoveAsync(refreshToken.ToCacheKey(), cancellationToken);
        await cacheService.SetRemoveAsync(refreshToken.ToUserCacheKey(), refreshToken.ToCacheKey(), cancellationToken);
        
        var response = new RefreshLoginResponse { AccessToken = tokens.AccessToken, RefreshId = tokens.RefreshToken.Id };

        if (!request.UseCookie)
        {
            logger.LogInformation("User {UserId} logged in without cookie.", user.Id);
            return response;
        }
        
        var cookieSignInResult = await cookieService.SignIn(tokens);

        if (cookieSignInResult.IsFail(out var cookieError))
        {
            logger.LogWarning("Failed to sign in user {UserId} with cookie: {Message}", user.Id, cookieError.Message);
            return cookieError;
        }

        logger.LogInformation("User {UserId} logged in with cookie.", user.Id);
        return response;
    }
}