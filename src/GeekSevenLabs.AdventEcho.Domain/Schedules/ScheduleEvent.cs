namespace GeekSevenLabs.AdventEcho.Domain.Schedules;

[HasPrivateEmptyConstructor]
public sealed partial class ScheduleEvent : Entity<ScheduleEvent, ScheduleEventId>
{
    public ScheduleEvent(string title, string description)
    {
        Title = title;
        Description = description;
        AddNotificationsAndThrow(new ScheduleEventValidationContract(this));
    }

    public string Title { get; private set; }
    public string Description { get; private set; }
}