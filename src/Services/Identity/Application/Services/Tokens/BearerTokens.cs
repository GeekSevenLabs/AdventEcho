using AdventEcho.Identity.Application.Shared;
using AdventEcho.Identity.Application.Shared.Accounts;

namespace AdventEcho.Identity.Application.Services.Tokens;

public record BearerTokens(BearerAccessToken AccessToken, BearerRefreshToken RefreshToken);