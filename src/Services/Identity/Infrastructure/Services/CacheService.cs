using AdventEcho.Identity.Application.Services.Caches;
using StackExchange.Redis;

namespace AdventEcho.Identity.Infrastructure.Services;

internal class CacheService(IConnectionMultiplexer redis) : ICacheService
{
    private readonly IDatabase _cache = redis.GetDatabase();
    
    public async Task SetStringAsync(string key, string value, TimeSpan? expiration = null, CancellationToken cancellationToken = default)
    {
        await _cache.StringSetAsync(key, value, expiration, when: When.Always);
    }

    public async Task SetAddAsync(string key, string value, CancellationToken cancellationToken = default)
    {
        await _cache.SetAddAsync(key, value);
    }

    public async Task<string?> GetStringAsync(string key, CancellationToken cancellationToken = default)
    {
        return await _cache.StringGetAsync(key);
    }

    public Task RemoveAsync(string key, CancellationToken cancellationToken = default)
    {
        return _cache.KeyDeleteAsync(key);
    }

    public Task SetRemoveAsync(string key, string value, CancellationToken cancellationToken = default)
    {
        return _cache.SetRemoveAsync(key, value);
    }
}