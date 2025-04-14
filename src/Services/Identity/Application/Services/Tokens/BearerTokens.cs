namespace AdventEcho.Identity.Application.Services.Tokens;

public record BearerTokens(BearerAccessToken AccessToken, BearerRefreshToken RefreshToken);