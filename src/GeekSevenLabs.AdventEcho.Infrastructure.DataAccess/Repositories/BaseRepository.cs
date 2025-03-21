using GeekSevenLabs.AdventEcho.Infrastructure.DataAccess.Contexts;
using GeekSevenLabs.AdventEcho.Kernel;
using GeekSevenLabs.AdventEcho.Kernel.Data;

namespace GeekSevenLabs.AdventEcho.Infrastructure.DataAccess.Repositories;

internal abstract class BaseRepository<TEntity, TKey>(AdventEchoDbContext db) : IRepository<TEntity> where TEntity : class, IAggregateRoot where TKey : struct
{
    protected AdventEchoDbContext Db => db;
    
    public void Add(TEntity entity) => db.Set<TEntity>().Add(entity);
    public void Remove(TEntity entity) => db.Set<TEntity>().Remove(entity);
    public async Task<TEntity?> GetAsync(TKey id) => await db.Set<TEntity>().FindAsync(id);
    
    public void Dispose()
    {
        throw new NotImplementedException();
    }

    public IUnitOfWork UnitOfWork => db;
}