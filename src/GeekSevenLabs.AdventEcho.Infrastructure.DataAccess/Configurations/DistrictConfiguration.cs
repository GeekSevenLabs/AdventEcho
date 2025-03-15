using GeekSevenLabs.AdventEcho.Domain.Districts;
using GeekSevenLabs.AdventEcho.Domain.People;

namespace GeekSevenLabs.AdventEcho.Infrastructure.DataAccess.Configurations;

internal class DistrictConfiguration : IEntityTypeConfiguration<District>
{
    public void Configure(EntityTypeBuilder<District> builder)
    {
        builder.HasKey(district => district.Id);
        builder.Property(district => district.Id).ValueGeneratedOnAdd();
        
        builder
            .Property(district => district.Name)
            .IsRequired()
            .HasMaxLength(150);

        builder
            .Property(district => district.PastorId)
            .IsRequired();

        builder
            .HasOne(district => district.Pastor)
            .WithOne()
            .HasForeignKey<District>(district => district.PastorId);

        builder
            .HasMany(district => district.Churches)
            .WithOne(church => church.District)
            .HasForeignKey(church => church.DistrictId);
    }
}