using AdventEcho.Identity.Domain.Users;
using AdventEcho.Kernel.Application.Shared.Messages.Results;

namespace AdventEcho.Identity.Application.Services.Tokens;

public interface IBearerTokenService
{
    Task<Result<BearerTokens>> GenerateTokensAsync(User user);
}