using GeekSevenLabs.AdventEcho.Domain.Shared;

namespace GeekSevenLabs.AdventEcho.Domain.Schedules;

[HasPrivateEmptyConstructor]
public sealed partial class ScheduledPerson
{
    public ScheduleDayId ScheduleDayId { get; private set; }
    public PersonId AssignedId { get; private set; }
    
    public ScheduleDayAssignmentId AssignmentId { get; private set; }
}