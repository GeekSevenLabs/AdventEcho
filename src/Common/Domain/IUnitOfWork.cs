// ReSharper disable once CheckNamespace
namespace AdventEcho;

public interface IUnitOfWork
{
    Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default);
}