using AdventEcho.Identity.Domain.Users;

namespace AdventEcho.Identity.Application.Services.Tokens;

public interface IBearerTokenService
{
    Task<Result<BearerTokens>> GenerateTokensAsync(User user);
}