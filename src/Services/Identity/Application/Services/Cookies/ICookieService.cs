using AdventEcho.Identity.Application.Services.Tokens;

namespace AdventEcho.Identity.Application.Services.Cookies;

public interface ICookieService
{
    Task<Result> SignIn(JwtTokens tokens);
    Task<Result> SignOut();
}