namespace GeekSevenLabs.AdventEcho.Domain.Noticies;

public class NoticeValidationContract : Contract<Notice>
{
    public const string TitleIsRequired = $"The '{nameof(Notice.Title)}' is required";
    public const string DescriptionIsRequired = $"The '{nameof(Notice.Description)}' is required";
    public const string PeriodIsRequired = $"The '{nameof(Notice.Period)}' is required";
    public const string DistrictIdOrChurchIdIsRequired = $"The '{nameof(Notice.DistrictId)}' or '{nameof(Notice.ChurchId)}' is required";
    public const string DistrictIdOrChurchIdNotBoth = $"The '{nameof(Notice.DistrictId)}' and '{nameof(Notice.ChurchId)}' can't be filled at the same time";
    
    public NoticeValidationContract(Notice notice)
    {
        Requires()
            .IsNotNullOrEmpty(notice.Title, nameof(notice.Title), TitleIsRequired)
            .IsNotNullOrEmpty(notice.Description, nameof(notice.Description), DescriptionIsRequired)
            .IsNotNull(notice.Period, nameof(notice.Period), PeriodIsRequired)
            .IsTrue(notice.DistrictId.HasValue || notice.ChurchId.HasValue, nameof(notice.DistrictId), DistrictIdOrChurchIdIsRequired)
            .IsFalse(notice is { DistrictId: not null, ChurchId: not null }, nameof(notice.DistrictId), DistrictIdOrChurchIdNotBoth);
    }
}