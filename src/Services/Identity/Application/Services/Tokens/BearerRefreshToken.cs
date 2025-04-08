using System.Text.Json;
using System.Text.Json.Serialization;
using AdventEcho.Kernel.Application.Errors;
using Cblx.Blocks;

namespace AdventEcho.Identity.Application.Services.Tokens;

[HasPrivateEmptyConstructor]
public sealed partial class BearerRefreshToken
{
    public BearerRefreshToken(Guid userId, DateTimeOffset expiration)
    {
        UserId = userId;
        Expiration = expiration;
    }
    
    [JsonInclude] public Guid Id { get; private set; } = Guid.NewGuid();
    [JsonInclude] public Guid UserId { get; private set; }
    [JsonInclude] public DateTimeOffset Expiration { get; private set; }
    
    
    public string ToJson() => JsonSerializer.Serialize(this);
    
    public static Result<BearerRefreshToken> FromJson(string json)
    {
        var token = JsonSerializer.Deserialize<BearerRefreshToken>(json);
        if (token == null) return SecurityErrors.Unauthorized;
        return token;
    }

    public string ToCacheKey() => nameof(BearerRefreshToken) + Id.ToString("N");
    public static string ToCacheKey(Guid id) => nameof(BearerRefreshToken) + id.ToString("N");
    public string ToUserCacheKey() => nameof(BearerRefreshToken)+":" + UserId.ToString("N");
    public static string ToUserCacheKey(Guid userId) => nameof(BearerRefreshToken)+":" + userId.ToString("N");

    public bool IsExpired()
    {
        var now = DateTimeOffset.UtcNow;
        return now > Expiration;
    }
}