using GeekSevenLabs.AdventEcho.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GeekSevenLabs.AdventEcho.Infrastructure.Identity.Contexts;

public class AdventEchoIdentityDbContext(DbContextOptions<AdventEchoIdentityDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder
            .Entity<ApplicationUser>()
            .Property(e => e.PersonId)
            .IsRequired(false);
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);

        configurationBuilder.Properties<PersonId>().HaveConversion<PersonId.EfCoreValueConverter>();
    }
}