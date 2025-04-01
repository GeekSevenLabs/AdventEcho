namespace AdventEcho.Kernel.Infrastructure.Options;

public class AdventEchoIdentityCookieOptions
{
    public static AdventEchoIdentityCookieOptions Default => new()
    {
        AccessTokenName = string.Empty,
        RefreshTokenName = string.Empty
    };

    public required string AccessTokenName { get; init; }
    public required string RefreshTokenName { get; init; }
}