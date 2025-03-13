using GeekSevenLabs.AdventEcho.Application.Shared;
using GeekSevenLabs.AdventEcho.Domain;
using GeekSevenLabs.AdventEcho.Domain.Shared;
using Microsoft.AspNetCore.Identity;
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
        
        SeedRoles(modelBuilder);
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);

        configurationBuilder.Properties<PersonId>().HaveConversion<PersonId.EfCoreValueConverter>();
    }
    
    private static void SeedRoles(ModelBuilder builder)
    {
        builder
            .Entity<IdentityRole>()
            .HasData(
            new IdentityRole { Id = StringsRoles.DeveloperId, Name = StringsRoles.Developer, NormalizedName = StringsRoles.DeveloperNormalized },
            new IdentityRole { Id = StringsRoles.AdministratorId, Name = StringsRoles.Administrator, NormalizedName = StringsRoles.AdministratorNormalized },
            new IdentityRole { Id = StringsRoles.PastorId, Name = StringsRoles.Pastor, NormalizedName = StringsRoles.PastorNormalized },
            new IdentityRole { Id = StringsRoles.ElderId, Name = StringsRoles.Elder, NormalizedName = StringsRoles.ElderNormalized },
            new IdentityRole { Id = StringsRoles.DirectorId, Name = StringsRoles.Director, NormalizedName = StringsRoles.DirectorNormalized },
            new IdentityRole { Id = StringsRoles.MemberId, Name = StringsRoles.Member, NormalizedName = StringsRoles.MemberNormalized }
        );
    }
}