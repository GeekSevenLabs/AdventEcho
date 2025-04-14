using AdventEcho.Identity.Application.Services.Cookies;
using AdventEcho.Identity.Application.Services.Tokens;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace AdventEcho.Identity.Infrastructure.Services;

internal class CookieService(IHttpContextAccessor accessor, IOptions<AdventEchoIdentityOptions> options) : ICookieService
{
    private readonly AdventEchoIdentityOptions _configs = options.Value;

    public async Task<Result> SetTokensIntoCookieAsync(BearerTokens tokens)
    {
        try
        {
            var context = accessor.HttpContext;
            if (context is null) return SecurityErrors.Unauthorized;
            
            context.RemoveTokensFromCookie(_configs);
            
            context.SetTokensIntoCookie(
                _configs,
                tokens.AccessToken, 
                tokens.RefreshToken.Id.ToString("N"),
                tokens.RefreshToken.Expiration);

            await Task.CompletedTask;
        }
        catch (Exception e)
        {
            return e;
        }

        return Result.Ok();
    }

    public async Task<Result> RemoveTokensFromCookieAsync()
    {
        try
        {
            var context = accessor.HttpContext;
            if(context is null) return SecurityErrors.Unauthorized;
            
            context.Response.Cookies.Delete(_configs.Cookie.AccessTokenName);
            context.Response.Cookies.Delete(_configs.Cookie.RefreshTokenName);

            await Task.CompletedTask;
        }
        catch (Exception e)
        {
            return e;
        }

        return Result.Ok();
    }
    
}