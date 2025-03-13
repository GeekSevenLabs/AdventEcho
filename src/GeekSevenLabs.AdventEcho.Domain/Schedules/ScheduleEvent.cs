namespace GeekSevenLabs.AdventEcho.Domain.Schedules;

[HasPrivateEmptyConstructor]
public sealed partial class ScheduleEvent : Entity<ScheduleEventId>
{
    public ScheduleEvent(string title, string description)
    {
        Title = title;
        Description = description;
    }

    public string Title { get; private set; }
    public string Description { get; private set; }
}