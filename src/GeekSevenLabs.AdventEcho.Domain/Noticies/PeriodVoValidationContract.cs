namespace GeekSevenLabs.AdventEcho.Domain.Noticies;

public class PeriodVoValidationContract : Contract<PeriodVo>
{
    public const string StartInIsRequiredMessage = $"The '{nameof(PeriodVo.StartIn)}' date is required";
    public const string EndInIsRequiredMessage = $"The '{nameof(PeriodVo.EndIn)}' date is required";
    public const string StartShouldBeLowerThanEndMessage = $"The '{nameof(PeriodVo.StartIn)}' date should be lower than '{nameof(PeriodVo.EndIn)}' date";
    
    public PeriodVoValidationContract(PeriodVo period)
    {
        Requires()
            .IsNotNull(period.StartIn, nameof(PeriodVo.StartIn), StartInIsRequiredMessage)
            .IsNotNull(period.EndIn, nameof(PeriodVo.EndIn), EndInIsRequiredMessage)
            .IsLowerThan(period.StartIn.DateTime, period.EndIn.DateTime, nameof(PeriodVo.StartIn) + nameof(PeriodVo.EndIn), StartShouldBeLowerThanEndMessage);
    }
}