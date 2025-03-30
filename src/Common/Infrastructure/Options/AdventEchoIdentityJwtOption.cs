namespace AdventEcho.Kernel.Infrastructure.Options;

public class AdventEchoIdentityJwtForClientOption
{
    public static AdventEchoIdentityJwtForClientOption Default => new()
    {
        Audiences = [],
        Issuer = string.Empty,
        PublicKey = string.Empty,
    };
    
    public required string PublicKey { get; init; }
    public required string Issuer { get; init; }
    public required string[] Audiences { get; init; } = [];
}