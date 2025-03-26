using AdventEcho.Identity.Application.Shared;
using AdventEcho.Identity.Domain.Users;

namespace AdventEcho.Identity.Application.Tokens;

public interface ITokenService
{
    Task<Result<JwtTokens>> GenerateTokensAsync(IUser user);
    Task<Result<JwtTokens>> RefreshTokenAsync(IUser user, JwtToken refreshToken);
}