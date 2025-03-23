using AdventEcho.Identity.Application.Shared;

namespace AdventEcho.Identity.Application.Tokens;

public interface ITokenService
{
    Task<Result<JwtTokens>> GenerateTokensAsync(IUser user);
    Task<Result<JwtTokens>> RefreshTokenAsync(IUser user, JwtToken refreshToken);
}