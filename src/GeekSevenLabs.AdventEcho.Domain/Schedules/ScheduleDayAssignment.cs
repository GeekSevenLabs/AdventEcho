using GeekSevenLabs.AdventEcho.Domain.Shared.Enums;

namespace GeekSevenLabs.AdventEcho.Domain.Schedules;

[HasPrivateEmptyConstructor]
public sealed partial class ScheduleDayAssignment : Entity<ScheduleDayAssignmentId>
{
    public ScheduleDayAssignment(string name, ScheduleType forScheduleType)
    {
        Name = name;
        ForScheduleType = forScheduleType;
    }

    public string Name { get; private set; }
    public ScheduleType ForScheduleType { get; private set; }
}