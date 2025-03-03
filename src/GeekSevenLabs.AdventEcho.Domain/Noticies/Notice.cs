namespace GeekSevenLabs.AdventEcho.Domain.Noticies;

[HasPrivateEmptyConstructor]
public sealed partial class Notice : Entity<Notice>
{
    public Notice(string title, string description, PeriodVo period, bool notifyEveryDay, Guid? districtId, Guid? churchId)
    {
        Title = title;
        Description = description;
        Period = period;
        NotifyEveryDay = notifyEveryDay;
        DistrictId = districtId;
        ChurchId = churchId;

        AddNotifications(period);
        AddNotificationsAndThrow(new NoticeValidationContract(this));
    }

    public string Title { get; private set; }
    public string Description { get; private set; }
    
    public PeriodVo Period { get; private set; }
    public bool NotifyEveryDay { get; private set; }

    public Guid? DistrictId { get; private set; }
    
    public Guid? ChurchId { get; private set; }
}