using GeekSevenLabs.AdventEcho.Domain.Noticies;

namespace GeekSevenLabs.AdventEcho.Domain.Erros;

public static class NoticeStrings
{
    public const string TitleIsRequired = $"The '{nameof(Notice.Title)}' is required";
    public const string DescriptionIsRequired = $"The '{nameof(Notice.Description)}' is required";
    public const string PeriodIsRequired = $"The '{nameof(Notice.Period)}' is required";
    public const string DistrictIdOrChurchIdIsRequired = $"The '{nameof(Notice.DistrictId)}' or '{nameof(Notice.ChurchId)}' is required";
    public const string DistrictIdOrChurchIdNotBoth = $"The '{nameof(Notice.DistrictId)}' and '{nameof(Notice.ChurchId)}' can't be filled at the same time";
}