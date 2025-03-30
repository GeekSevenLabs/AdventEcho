namespace AdventEcho.Kernel.Infrastructure.Options;

public class AdventEchoIdentityCookieOption
{
    public static AdventEchoIdentityCookieOption Default => new()
    {
        AccessTokenName = string.Empty,
        RefreshIdName = string.Empty,
        LoginPath = string.Empty,
        LogoutPath = string.Empty
    };
    
    
    public required string AccessTokenName { get; init; }
    public required string RefreshIdName { get; init; }
    public required string LoginPath { get; init; }
    public required string LogoutPath { get; init; }
}