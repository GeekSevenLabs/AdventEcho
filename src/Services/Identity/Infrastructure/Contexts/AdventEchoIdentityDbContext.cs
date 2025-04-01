using AdventEcho.Identity.Domain;
using AdventEcho.Identity.Domain.Users;
using AdventEcho.Identity.Infrastructure.Configurations.Domain;

namespace AdventEcho.Identity.Infrastructure.Contexts;

public class AdventEchoIdentityDbContext(DbContextOptions<AdventEchoIdentityDbContext> options) : DbContext(options), IAdventEchoIdentityUnitOfWork
{
    
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new UserConfiguration());
    }

    public new async Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var rowsAffected = await base.SaveChangesAsync(cancellationToken);
        return rowsAffected > 0;
    }
}