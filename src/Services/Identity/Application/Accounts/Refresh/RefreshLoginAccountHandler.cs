using AdventEcho.Identity.Application.Services.Caches;
using AdventEcho.Identity.Application.Services.Cookies;
using AdventEcho.Identity.Application.Services.Tokens;
using AdventEcho.Identity.Application.Shared.Accounts.Refresh;
using AdventEcho.Identity.Domain.Users;
using AdventEcho.Kernel.Application.Handlers;
using AdventEcho.Kernel.Application.Shared.Messages.Results;
using Microsoft.Extensions.Logging;

namespace AdventEcho.Identity.Application.Accounts.Refresh;

public class RefreshLoginAccountHandler(
    IBearerTokenService tokenService,
    ICacheService cacheService,
    ICookieService cookieService,
    IUserRepository userRepository,
    ILogger<RefreshLoginAccountHandler> logger)
    : ICommandHandler<RefreshLoginAccountRequest, RefreshLoginAccountResponse>
{
    public async Task<Result<RefreshLoginAccountResponse>> Handle(RefreshLoginAccountRequest request, CancellationToken cancellationToken)
    {
        var refreshId = Guid.Parse(request.RefreshToken);
        
        var refreshTokenJson = await cacheService.GetStringAsync(BearerRefreshToken.ToCacheKey(refreshId), cancellationToken);
        if (string.IsNullOrEmpty(refreshTokenJson))
        {
            logger.LogWarning("Refresh token not found in cache");
            return EchoResults<RefreshLoginAccountResponse>.Unauthorized();
        }
        
        var refreshTokenResult = BearerRefreshToken.FromJson(refreshTokenJson);
        if (refreshTokenResult.IsFail(out var refreshToken, out var refreshTokenError)) return refreshTokenError;
        
        if (refreshToken.IsExpired())
        {
            logger.LogWarning("Refresh token expired or invalid for user {UserId}", refreshToken.UserId);
            await cacheService.RemoveAsync(refreshToken.ToCacheKey(), cancellationToken);
            return EchoResults<RefreshLoginAccountResponse>.Unauthorized();
        }
        
        var user = await userRepository.GetByIdAsync(refreshToken.UserId);
        if (user is null)
        {
            logger.LogWarning("User not found for refresh token {RefreshId}", request.RefreshToken);
            await cacheService.RemoveAsync(refreshToken.ToCacheKey(), cancellationToken);
            return EchoResults<RefreshLoginAccountResponse>.Unauthorized();
        }
        
        var tokensResult = await tokenService.GenerateTokensAsync(user);
        if (tokensResult.IsFail(out var tokens, out var tokensError))
        {
            logger.LogWarning("Failed to generate tokens for user {UserId}: {Message}", user.Id, tokensError.Message);
            return tokensError;
        }
        
        var expiration = tokens.RefreshToken.Expiration - DateTime.UtcNow;
        
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
        
        var response = new RefreshLoginAccountResponse { AccessToken = tokens.AccessToken, RefreshToken = tokens.RefreshToken.Id.ToString("N") };

        if (request.UseCookie is false)
        {
            logger.LogInformation("User {UserId} logged in without cookie.", user.Id);

            await cookieService.RemoveTokensFromCookieAsync();
            return EchoResults<RefreshLoginAccountResponse>.Success(response);
        }
        
        var setTokensResult = await cookieService.SetTokensIntoCookieAsync(tokens);

        if (setTokensResult.IsFail(out var cookieError))
        {
            logger.LogWarning("Failed to sign in user {UserId} with cookie: {Message}", user.Id, cookieError.Message);
            return EchoResults<RefreshLoginAccountResponse>.Fail(cookieError);
        }

        logger.LogInformation("User {UserId} logged in with cookie.", user.Id);
        return EchoResults<RefreshLoginAccountResponse>.Success(response);
    }
}