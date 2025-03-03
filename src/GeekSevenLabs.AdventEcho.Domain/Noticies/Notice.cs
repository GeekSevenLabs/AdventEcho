namespace GeekSevenLabs.AdventEcho.Domain.Noticies;

[HasPrivateEmptyConstructor]
public sealed partial class Notice : Entity<Notice, NoticeId>
{
    public Notice(string title, string description, PeriodVo period, bool notifyEveryDay, DistrictId districtId)
    {
        Title = title;
        Description = description;
        Period = period;
        NotifyEveryDay = notifyEveryDay;
        DistrictId = districtId;
        ChurchId = null;

        AddNotifications(period);
        AddNotificationsAndThrow(new NoticeValidationContract(this));
    }
    
    public Notice(string title, string description, PeriodVo period, bool notifyEveryDay, ChurchId churchId)
    {
        Title = title;
        Description = description;
        Period = period;
        NotifyEveryDay = notifyEveryDay;
        DistrictId = null;
        ChurchId = churchId;

        AddNotifications(period);
        AddNotificationsAndThrow(new NoticeValidationContract(this));
    }

    public string Title { get; private set; }
    public string Description { get; private set; }
    
    public PeriodVo Period { get; private set; }
    public bool NotifyEveryDay { get; private set; }

    public DistrictId? DistrictId { get; private set; }
    public ChurchId? ChurchId { get; private set; }
}