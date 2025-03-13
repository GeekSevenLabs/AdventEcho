using GeekSevenLabs.AdventEcho.Domain.Noticies;

namespace GeekSevenLabs.AdventEcho.Domain.Erros;

public class PeriodStrings
{
    public const string StartInIsRequiredMessage = $"The '{nameof(PeriodVo.StartIn)}' date is required";
    public const string EndInIsRequiredMessage = $"The '{nameof(PeriodVo.EndIn)}' date is required";
    public const string StartShouldBeLowerThanEndMessage = $"The '{nameof(PeriodVo.StartIn)}' date should be lower than '{nameof(PeriodVo.EndIn)}' date";
}