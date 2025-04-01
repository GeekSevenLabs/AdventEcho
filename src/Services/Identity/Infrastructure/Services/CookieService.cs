using AdventEcho.Identity.Application.Services.Cookies;
using AdventEcho.Identity.Application.Services.Tokens;
using AdventEcho.Kernel.Application.Shared.Messages.Results;
using AdventEcho.Kernel.Infrastructure.Options;
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
            if(context is null) return EchoResults.Unauthorized();
            
            context.Response.Cookies.Append(
                _configs.Cookie.AccessTokenName, 
                tokens.AccessToken.Value,
                DefaultCookieOptions(tokens.AccessToken.Expiration)
            );
            
            context.Response.Cookies.Append(
                _configs.Cookie.RefreshTokenName, 
                tokens.RefreshToken.Id.ToString(),
                DefaultCookieOptions(tokens.RefreshToken.Expiration)
            );

            await Task.CompletedTask;
        }
        catch (Exception e)
        {
            return EchoResults.Fail(e);
        }

        return EchoResults.Success();
    }

    public async Task<Result> RemoveTokensFromCookieAsync()
    {
        try
        {
            var context = accessor.HttpContext;
            if(context is null) return EchoResults.Unauthorized();
            
            context.Response.Cookies.Delete(_configs.Cookie.AccessTokenName);
            context.Response.Cookies.Delete(_configs.Cookie.RefreshTokenName);

            await Task.CompletedTask;
        }
        catch (Exception e)
        {
            return EchoResults.Fail(e);
        }

        return EchoResults.Success();
    }


    private static CookieOptions DefaultCookieOptions(DateTimeOffset expiration) => new()
    {
        HttpOnly = true,
        Secure = true,
        SameSite = SameSiteMode.None,
        Expires = expiration
    };
}