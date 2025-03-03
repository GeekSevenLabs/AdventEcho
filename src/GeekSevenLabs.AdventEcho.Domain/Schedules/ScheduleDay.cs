namespace GeekSevenLabs.AdventEcho.Domain.Schedules;

[HasPrivateEmptyConstructor]
public sealed partial class ScheduleDay : Entity<ScheduleDay, ScheduleDayId>
{
    public List<ScheduledPerson> _scheduledPeople = [];
    
    public DateOnly Day { get; private set; }
    
    public ScheduleId ScheduleId { get; private set; }
    public ScheduleEventId EventId { get; private set; }
    
    public IReadOnlyList<ScheduledPerson> ScheduledPeople => _scheduledPeople.AsReadOnly();
}