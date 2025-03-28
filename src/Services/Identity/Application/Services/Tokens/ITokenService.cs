using AdventEcho.Identity.Domain.Users;

namespace AdventEcho.Identity.Application.Services.Tokens;

public interface ITokenService
{
    Task<Result<JwtTokens>> GenerateTokensAsync(IUser user);
}