using AdventEcho.Identity.Application.Services.Tokens;

namespace AdventEcho.Identity.Application.Services.Cookies;

public interface ICookieService
{
    Task<Result> SetTokensIntoCookieAsync(BearerTokens tokens);
    Task<Result> RemoveTokensFromCookieAsync();
}