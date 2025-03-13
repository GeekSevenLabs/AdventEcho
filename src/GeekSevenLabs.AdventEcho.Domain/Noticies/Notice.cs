namespace GeekSevenLabs.AdventEcho.Domain.Noticies;

[HasPrivateEmptyConstructor]
public sealed partial class Notice : Entity<NoticeId>
{
    public Notice(string title, string description, PeriodVo period, bool notifyEveryDay, DistrictId districtId)
    {
        Title = title;
        Description = description;
        Period = period;
        NotifyEveryDay = notifyEveryDay;
        DistrictId = districtId;
        ChurchId = null;

        // .IsNotNullOrEmpty(notice.Title, nameof(notice.Title), TitleIsRequired)
        // .IsNotNullOrEmpty(notice.Description, nameof(notice.Description), DescriptionIsRequired)
        // .IsNotNull(notice.Period, nameof(notice.Period), PeriodIsRequired)
        // .IsTrue(notice.DistrictId.HasValue || notice.ChurchId.HasValue, nameof(notice.DistrictId), DistrictIdOrChurchIdIsRequired)
        // .IsFalse(notice is { DistrictId: not null, ChurchId: not null }, nameof(notice.DistrictId), DistrictIdOrChurchIdNotBoth);
    }
    
    public Notice(string title, string description, PeriodVo period, bool notifyEveryDay, ChurchId churchId)
    {
        Title = title;
        Description = description;
        Period = period;
        NotifyEveryDay = notifyEveryDay;
        DistrictId = null;
        ChurchId = churchId;

        // .IsNotNullOrEmpty(notice.Title, nameof(notice.Title), TitleIsRequired)
        // .IsNotNullOrEmpty(notice.Description, nameof(notice.Description), DescriptionIsRequired)
        // .IsNotNull(notice.Period, nameof(notice.Period), PeriodIsRequired)
        // .IsTrue(notice.DistrictId.HasValue || notice.ChurchId.HasValue, nameof(notice.DistrictId), DistrictIdOrChurchIdIsRequired)
        // .IsFalse(notice is { DistrictId: not null, ChurchId: not null }, nameof(notice.DistrictId), DistrictIdOrChurchIdNotBoth);
    }

    public string Title { get; private set; }
    public string Description { get; private set; }
    
    public PeriodVo Period { get; private set; }
    public bool NotifyEveryDay { get; private set; }

    public DistrictId? DistrictId { get; private set; }
    public ChurchId? ChurchId { get; private set; }
}