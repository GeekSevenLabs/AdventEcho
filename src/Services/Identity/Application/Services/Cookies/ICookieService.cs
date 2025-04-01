using AdventEcho.Identity.Application.Services.Tokens;
using AdventEcho.Kernel.Application.Shared.Messages.Results;

namespace AdventEcho.Identity.Application.Services.Cookies;

public interface ICookieService
{
    Task<Result> SetTokensIntoCookieAsync(BearerTokens tokens);
    Task<Result> RemoveTokensFromCookieAsync();
}