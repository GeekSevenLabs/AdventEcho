using AdventEcho.Identity.Application.Shared;
using AdventEcho.Identity.Application.Shared.Accounts;
using AdventEcho.Kernel.Application.Shared;

namespace AdventEcho.Identity.Application.Services.Tokens;

public record BearerTokens(BearerAccessToken AccessToken, BearerRefreshToken RefreshToken);