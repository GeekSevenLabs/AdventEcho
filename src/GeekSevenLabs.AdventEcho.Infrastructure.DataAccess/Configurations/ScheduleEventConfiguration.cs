using GeekSevenLabs.AdventEcho.Domain.Schedules;

namespace GeekSevenLabs.AdventEcho.Infrastructure.DataAccess.Configurations;

internal class ScheduleEventConfiguration : IEntityTypeConfiguration<ScheduleEvent>
{
    public void Configure(EntityTypeBuilder<ScheduleEvent> builder)
    {
        builder.HasKey(scheduleEvent => scheduleEvent.Id);
        builder.Property(scheduleEvent => scheduleEvent.Id).ValueGeneratedOnAdd();
        
        builder
            .Property(scheduleEvent => scheduleEvent.Title)
            .IsRequired()
            .HasMaxLength(150);
        
        builder
            .Property(scheduleEvent => scheduleEvent.Description)
            .IsRequired()
            .HasMaxLength(500);
    }
}