using Flunt.Notifications;

namespace GeekSevenLabs.AdventEcho.Domain;

public abstract record RecordNotifiable
{
    private readonly List<Notification> _notifications = [];

    public IReadOnlyCollection<Notification> Notifications => _notifications.AsReadOnly();

    private Notification GetNotificationInstance(string key, string message) => new(key, message);

    public void AddNotification(string key, string message) => _notifications.Add(GetNotificationInstance(key, message));

    public void AddNotification(Notification notification) => _notifications.Add(notification);

    public void AddNotification(Type property, string message)
    {
        var notification = GetNotificationInstance(property.Name, message);
        _notifications.Add(notification);
    }

    public void AddNotifications(IReadOnlyCollection<Notification> notifications) => _notifications.AddRange(notifications);

    public void AddNotifications(IList<Notification> notifications) => _notifications.AddRange(notifications);

    public void AddNotifications(ICollection<Notification> notifications) => _notifications.AddRange(notifications);

    public void AddNotifications(Notifiable<Notification> item) => AddNotifications(item.Notifications);

    public void AddNotifications(params Notifiable<Notification>[] items)
    {
        foreach (var item in items) AddNotifications(item);
    }

    public void Clear() => _notifications.Clear();

    public bool IsValid => _notifications.Count is 0;
}