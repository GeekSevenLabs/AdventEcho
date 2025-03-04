using GeekSevenLabs.AdventEcho.Domain.Churches;
using GeekSevenLabs.AdventEcho.Domain.Districts;
using GeekSevenLabs.AdventEcho.Domain.Schedules;

namespace GeekSevenLabs.AdventEcho.Infrastructure.DataAccess.Configurations;

internal class ScheduleConfiguration : IEntityTypeConfiguration<Schedule>
{
    public void Configure(EntityTypeBuilder<Schedule> builder)
    {
        builder.HasKey(schedule => schedule.Id);
        builder.Property(schedule => schedule.Id).ValueGeneratedOnAdd();
        
        builder.Property(schedule => schedule.Type).IsRequired();
        builder.Property(schedule => schedule.Status).IsRequired();
        
        builder.Property(schedule => schedule.DistrictId).IsRequired(false);
        builder.Property(schedule => schedule.ChurchId).IsRequired(false);
        
        builder
            .HasOne<District>()
            .WithMany()
            .HasForeignKey(schedule => schedule.DistrictId);
        
        builder
            .HasOne<Church>()
            .WithMany()
            .HasForeignKey(schedule => schedule.ChurchId);
        
        builder
            .HasMany(schedule => schedule.Days)
            .WithOne(day => day.Schedule)
            .HasForeignKey(day => day.ScheduleId);

    }
}