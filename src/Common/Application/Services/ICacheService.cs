namespace AdventEcho.Kernel.Application.Services;

public interface ICacheService
{
    Task SetStringAsync(string key, string value, TimeSpan? expiration = null, CancellationToken cancellationToken = default);
    Task SetAddAsync(string key, string value, CancellationToken cancellationToken = default);
    
    Task<string?> GetStringAsync(string key, CancellationToken cancellationToken = default);
    
    Task RemoveAsync(string key, CancellationToken cancellationToken = default);
    Task SetRemoveAsync(string key, string value, CancellationToken cancellationToken = default);
}