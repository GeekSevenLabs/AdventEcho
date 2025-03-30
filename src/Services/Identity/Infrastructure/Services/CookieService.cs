using AdventEcho.Identity.Application.Services.Cookies;
using AdventEcho.Identity.Application.Services.Tokens;
using AdventEcho.Kernel.Extensions;
using AdventEcho.Kernel.Messages;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace AdventEcho.Identity.Infrastructure.Services;

internal class CookieService(IHttpContextAccessor accessor, IOptions<AdventEchoIdentityOption> options) : ICookieService
{
    private readonly AdventEchoIdentityOption _configs = options.Value;

    public async Task<Result> SignIn(JwtTokens tokens)
    {
        try
        {
            var context = accessor.HttpContext;
            if(context is null) return "Invalid operation".ToInvalidOperationException();
            
            context.Response.Cookies.Append(
                _configs.Cookie.AccessTokenName, 
                tokens.AccessToken.Value,
                DefaultCookieOptions(tokens.AccessToken.Expires)
            );
            
            context.Response.Cookies.Append(
                _configs.Cookie.RefreshIdName, 
                tokens.RefreshToken.Id.ToString(),
                DefaultCookieOptions(tokens.RefreshToken.Expires)
            );

            await Task.CompletedTask;
        }
        catch (Exception e)
        {
            return e;
        }

        return Result.Success();
    }

    public Task<Result> SignOut()
    {
        throw new NotImplementedException();
    }
    
    private static CookieOptions DefaultCookieOptions(DateTime expires) => new()
    {
        HttpOnly = true,
        Secure = true,
        SameSite = SameSiteMode.Strict,
        Expires = expires
    };
}