using GeekSevenLabs.AdventEcho.Domain.People;

namespace GeekSevenLabs.AdventEcho.Domain.Schedules;

[HasPrivateEmptyConstructor]
public sealed partial class ScheduleDay : Entity<ScheduleDay, ScheduleDayId>
{
    public List<ScheduledPerson> _scheduledPeople = [];
    public List<Person> _people = [];
    
    public DateOnly Day { get; private set; }
    
    public ScheduleId ScheduleId { get; private set; }
    public ScheduleEventId? EventId { get; private set; }
    
    public IReadOnlyList<ScheduledPerson> ScheduledPeople => _scheduledPeople.AsReadOnly();
    public IReadOnlyList<Person> People => _people.AsReadOnly();
}