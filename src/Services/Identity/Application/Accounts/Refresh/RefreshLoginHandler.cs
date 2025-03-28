using AdventEcho.Identity.Application.Services.Tokens;
using AdventEcho.Identity.Application.Shared.Accounts.Refresh;
using AdventEcho.Identity.Domain.Users.Services;

namespace AdventEcho.Identity.Application.Accounts.Refresh;

internal class RefreshLoginHandler(ITokenService tokenService, IUserService userService) : IRefreshLoginHandler
{
    public async Task<Result<RefreshLoginResponse>> HandleAsync(RefreshLoginRequest request, CancellationToken cancellationToken = default)
    {
        var currentUserResult = await userService.GetUserIdAsync(request.UserId, cancellationToken);
        if (currentUserResult.IsFail(out var currentUser, out var error))  return Result<RefreshLoginResponse>.Fail(error);
        
        var tokenResult = await tokenService.GenerateTokensAsync(currentUser);
        
        return tokenResult.Match(Success, Result<RefreshLoginResponse>.Fail);
    }

    private static Result<RefreshLoginResponse> Success(JwtTokens tokens)
    {
        var response = new RefreshLoginResponse
        {
            AccessToken = tokens.Access,
            RefreshToken = tokens.Refresh,
        };
        return Result<RefreshLoginResponse>.Success(response);
    }
}