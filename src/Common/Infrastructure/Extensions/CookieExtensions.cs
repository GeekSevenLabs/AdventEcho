using Microsoft.AspNetCore.Http;

// ReSharper disable once CheckNamespace
namespace AdventEcho;

public static class CookieExtensions
{
    public static void RemoveTokensFromCookie(this HttpContext context, AdventEchoIdentityOptions options)
    {
        context.Response.Cookies.Delete(options.Cookie.AccessTokenName);
        context.Response.Cookies.Delete(options.Cookie.RefreshTokenName);
    }
    
    public static void SetTokensIntoCookie(
        this HttpContext context, 
        AdventEchoIdentityOptions options,
        BearerAccessToken accessToken,
        string refreshToken,
        DateTimeOffset refreshTokenExpiration)
    {
        context.Response.Cookies.Append(
            options.Cookie.AccessTokenName,
            accessToken.Value,
            DefaultCookieOptions(accessToken.Expiration)
        );

        context.Response.Cookies.Append(
            options.Cookie.RefreshTokenName,
            refreshToken,
            DefaultCookieOptions(refreshTokenExpiration)
        );
    }
    
    private static CookieOptions DefaultCookieOptions(DateTimeOffset expiration) => new()
    {
        HttpOnly = true,
        Secure = true,
        SameSite = SameSiteMode.None,
        Expires = expiration
    };
}