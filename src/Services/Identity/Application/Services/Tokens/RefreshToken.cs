using System.Text.Json;
using System.Text.Json.Serialization;
using AdventEcho.Kernel.Extensions;

namespace AdventEcho.Identity.Application.Services.Tokens;

public class RefreshToken
{
    private RefreshToken()
    {
        
    }
    
    public RefreshToken(Guid userId, string email, DateTime expires)
    {
        UserId = userId;
        Expires = expires;
        Email = email;
    }
    
    [JsonInclude] public Guid Id { get; private set; } = Guid.NewGuid();
    [JsonInclude] public Guid UserId { get; private set; }
    [JsonInclude] public string Email { get; private set; } = string.Empty;
    [JsonInclude] public DateTime Expires { get; private set; }
    
    public string ToJson() => JsonSerializer.Serialize(this);
    
    public static Result<RefreshToken> FromJson(string json)
    {
        var token = JsonSerializer.Deserialize<RefreshToken>(json);
        if (token is null) return "Failed to deserialize refresh token.".ToInvalidOperationException();
        return token;
    }

    public string ToCacheKey() => nameof(RefreshToken) + Id;
    public static string ToCacheKey(Guid id) => nameof(RefreshToken) + id;
    public string ToUserCacheKey() => nameof(RefreshToken)+":" + UserId;
    public static string ToUserCacheKey(Guid userId) => nameof(RefreshToken)+":" + userId;

    public bool IsExpired()
    {
        var now = DateTime.UtcNow;
        return now > Expires;
    }
}