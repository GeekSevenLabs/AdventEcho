using AdventEcho.Identity.Domain.Users;

namespace AdventEcho.Identity.Infrastructure.Configurations;

internal class UserConfiguration : IEntityTypeConfiguration<AdventEchoUser>
{
    public void Configure(EntityTypeBuilder<AdventEchoUser> builder)
    {
        // Primary key
        builder.HasKey(user => user.Id);

        // Indexes for "normalized" username and email, to allow efficient lookups
        builder.HasIndex(user => user.NormalizedUserName).HasDatabaseName("UserNameIndex").IsUnique();
        builder.HasIndex(user => user.NormalizedEmail).HasDatabaseName("EmailIndex");

        // Maps to the AspNetUsers table
        builder.ToTable("Users");

        // A concurrency token for use with the optimistic concurrency checking
        builder.Property(user => user.ConcurrencyStamp).IsConcurrencyToken();

        // Limit the size of columns to use efficient database types
        builder.Property(user => user.UserName).HasMaxLength(256).IsRequired();
        builder.Property(user => user.NormalizedUserName).HasMaxLength(256).IsRequired();
        builder.Property(user => user.Email).HasMaxLength(256).IsRequired();
        builder.Property(user => user.NormalizedEmail).HasMaxLength(256).IsRequired();
        builder.Property(user => user.PhoneNumber).HasMaxLength(256);
        
        // Complex types
        // The following properties are complex types, so we need to configure them
        builder.ComplexProperty<NameVo>(user => user.Name, voBuilder =>
        {
            // Configure the properties of the complex type
            voBuilder.Property(name => name.First).HasMaxLength(100).IsRequired();
            voBuilder.Property(name => name.Last).HasMaxLength(150).IsRequired();
        });

        // The relationships between User and other entity types
        // Note that these relationships are configured with no navigation properties

        // Each User can have many UserClaims
        builder.HasMany(user => user.Claims).WithOne(claim => claim.User).HasForeignKey(uc => uc.UserId).HasPrincipalKey(user => user.Id).IsRequired();

        // Each User can have many UserLogins
        builder.HasMany(user => user.Logins).WithOne(login => login.User).HasForeignKey(ul => ul.UserId).HasPrincipalKey(user => user.Id).IsRequired();

        // Each User can have many UserTokens
        builder.HasMany(user => user.Tokens).WithOne(token => token.User).HasForeignKey(ut => ut.UserId).HasPrincipalKey(user => user.Id).IsRequired();

        // Each User can have many entries in the UserRole join table
        builder.HasMany(user => user.UserRoles).WithOne(ur => ur.User).HasForeignKey(ur => ur.UserId).IsRequired();
    }
}