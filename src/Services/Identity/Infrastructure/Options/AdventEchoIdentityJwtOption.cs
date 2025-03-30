namespace AdventEcho.Identity.Infrastructure.Options;

public class AdventEchoIdentityJwtOption
{
    public static AdventEchoIdentityJwtOption Default => new()
    {
        Audiences = [],
        Issuer = string.Empty,
        Secret = string.Empty,
        PrivateKey = string.Empty,
        PublicKey = string.Empty,
        ExpireInMinutes = 0,
        RefreshTokenExpireInMinutes = 0
    };
    
    public required string Secret { get; init; }
    public required string PrivateKey { get; init; }
    public required string PublicKey { get; init; }
    public required string Issuer { get; init; }
    public required string[] Audiences { get; init; } = [];
    public required int ExpireInMinutes { get; init; }
    public required int RefreshTokenExpireInMinutes { get; init; }

    public DateTime GetAccessTokenLifetime()
    {
        return DateTime.UtcNow.AddMinutes(ExpireInMinutes);
    }

    public DateTime GetRefreshTokenLifetime()
    {
        return DateTime.UtcNow.AddMinutes(RefreshTokenExpireInMinutes);
    }
}