using AdventEcho.Identity.Domain.Users;

namespace AdventEcho.Identity.Infrastructure.Repositories;

internal class UserRepository(AdventEchoIdentityDbContext db) : Repository<User>(db), IUserRepository
{
    public async Task<User?> GetByIdAsync(Guid id) => await Db.Users.FindAsync(id);
}