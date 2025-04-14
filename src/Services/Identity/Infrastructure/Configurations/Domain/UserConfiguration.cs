using AdventEcho.Identity.Domain.Users;

namespace AdventEcho.Identity.Infrastructure.Configurations.Domain;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
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
        
        // Complex types
        // The following properties are complex types, so we need to configure them
        builder.ComplexProperty<NameVo>(user => user.Name, voBuilder =>
        {
            // Configure the properties of the complex type
            voBuilder.Property(name => name.First).HasMaxLength(100).IsRequired();
            voBuilder.Property(name => name.Last).HasMaxLength(150).IsRequired();
        });
    }
}