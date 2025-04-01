namespace AdventEcho.Kernel.Infrastructure.Options;

public class AdventEchoIdentityBearerOptions
{
    public static AdventEchoIdentityBearerOptions Default => new()
    {
        Audiences = [],
        Issuer = string.Empty,
        PublicKey = string.Empty,
        AccessTokenExpirationInMinutes = 0,
        RefreshTokenExpirationInMinutes = 0
    };

    public required string Issuer { get; init; }
    public required string[] Audiences { get; init; }
    public required string PublicKey { get; init; }

    public int AccessTokenExpirationInMinutes { get; set; }
    public int RefreshTokenExpirationInMinutes { get; set; }
}