using AdventEcho.Kernel.Domain.Entities;

namespace AdventEcho.Identity.Infrastructure.Repositories;

internal abstract class Repository<TEntity>(AdventEchoIdentityDbContext db) where TEntity : class, IAggregateRoot
{
    protected AdventEchoIdentityDbContext Db => db;
}