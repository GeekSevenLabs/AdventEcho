using System.Text.Json.Serialization;
using AdventEcho.Kernel.Infrastructure.Options;

namespace AdventEcho.Identity.Infrastructure.Options;

public class AdventEchoIdentityOption
{
    public const string SectionName = "AdventEchoIdentity";
    
    public static class Db
    {
        public const int MaxKeyLength = 128;
        public const string ConnectionStringName = "AdventEchoIdentityConnection";
    }

    public static class Cors
    {
        public const string PolicyName = "AdventEchoCorsPolicy";
    }
    
    public static class Redis
    {
        public const string ConnectionStringName = "AdventEchoRedisConnection";
        public const string InstanceName = "AdventEchoIdentity_";
    }
    
    [JsonInclude]
    public AdventEchoIdentityDomainsOption Domains { get; set; } = AdventEchoIdentityDomainsOption.Default;
    
    [JsonInclude]
    public AdventEchoIdentityJwtOption Jwt { get; set; } = AdventEchoIdentityJwtOption.Default;
    
    [JsonInclude]
    public AdventEchoIdentityCookieOption Cookie { get; set; } = AdventEchoIdentityCookieOption.Default;
}