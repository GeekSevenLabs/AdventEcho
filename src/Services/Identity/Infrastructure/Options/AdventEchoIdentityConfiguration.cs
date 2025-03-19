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
}


public class AdventEchoIdentityDomainsConfiguration
{
    public const string SectionName = "AdventEchoIdentity:Domains";
    
    public required string ApiIdentity { get; set; }
}