using AdventEcho.Identity.Application.Services.Tokens;
using AdventEcho.Identity.Application.Shared.Accounts.Login;
using AdventEcho.Identity.Domain.Users;
using AdventEcho.Identity.Domain.Users.Services;

namespace AdventEcho.Identity.Application.Accounts.Login;

internal class UserLoginHandler(IUserService userService, ITokenService tokenService) : IUserLoginHandler
{
    public async Task<Result<LoginResponse>> HandleAsync(LoginRequest request, CancellationToken cancellationToken = default)
    {
        var userResult = await userService.CheckPasswordSignInAsync(request.Email!, request.Password!, cancellationToken);
        return await userResult.MatchAsync(GenerateTokenAsync, Result<LoginResponse>.Fail);
    }
    
    private async Task<Result<LoginResponse>> GenerateTokenAsync(IUser user)
    {
        var tokenResult = await tokenService.GenerateTokensAsync(user);
        return tokenResult.Match(CreateLoginResponse, Result<LoginResponse>.Fail);
    }
    
    private static Result<LoginResponse> CreateLoginResponse(JwtTokens tokens)
    {
        return new LoginResponse
        {
            AccessToken = tokens.Access,
            RefreshToken = tokens.Refresh
        };
    }
}