namespace GeekSevenLabs.AdventEcho.Domain.Schedules;

public class ScheduleEventValidationContract : Contract<ScheduleEvent>
{
    public const string TitleRequiredMessage = $"The '{nameof(ScheduleEvent.Title)}' field is required";
    public const string TitleIsLassThanThreeCharactersMessage = $"The '{nameof(ScheduleEvent.Title)}' field must be at least 5 characters";
    public const string DescriptionRequiredMessage = $"The '{nameof(ScheduleEvent.Title)}' field is required";
    
    public ScheduleEventValidationContract(ScheduleEvent scheduleEvent)
    {
        Requires()
            .IsNotNullOrEmpty(scheduleEvent.Title, nameof(ScheduleEvent.Title), TitleRequiredMessage)
            .IsGreaterOrEqualsThan(scheduleEvent.Title, 5 , nameof(ScheduleEvent.Title), TitleIsLassThanThreeCharactersMessage)
            .IsNotNullOrEmpty(scheduleEvent.Title, nameof(ScheduleEvent.Description),DescriptionRequiredMessage);
    }
}