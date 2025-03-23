namespace AdventEcho.Identity.Infrastructure.Options;

public static class AdventEchoIdentityConfiguration
{
    public const int MaxKeyLength = 128;
    public const string ConnectionStringName = "AdventEchoIdentityConnection";
}

public class AdventEchoIdentityJwtConfiguration
{
    public const string SectionName = "AdventEchoIdentity:Jwt";
    
    public required string Secret { get; set; }
    public required string Issuer { get; set; }
    public required string[] Audiences { get; set; } = [];

    public required int AccessTokenLifetime { get; set; }
    public required int RefreshTokenLifetime { get; set; }

    public (DateTime ValidIn, DateTime ExpiresIn) GetAccessTokenLifetime()
    {
        return (DateTime.UtcNow, DateTime.Now.AddMinutes(AccessTokenLifetime));
    }

    public (DateTime ValidIn, DateTime ExpiresIn) GetRefreshTokenLifetime(DateTime accessTokenExpiration)
    {
        var validIn = accessTokenExpiration.AddMinutes(-5);
        var expires = validIn.AddMinutes(RefreshTokenLifetime);
        return (validIn, expires);
    }
}


public class AdventEchoIdentityDomainsConfiguration
{
    public const string SectionName = "AdventEchoIdentity:Domains";
    
    public required string ApiIdentity { get; set; }
}