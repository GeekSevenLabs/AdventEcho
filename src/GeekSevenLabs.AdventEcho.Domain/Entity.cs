using Flunt.Notifications;

namespace GeekSevenLabs.AdventEcho.Domain;

public abstract class Entity<TEntity> : Notifiable<Notification>
{
    public Guid Id { get; protected set; }
    
    protected virtual void AddNotificationsAndThrow(Contract<TEntity> contract)
    {
        AddNotifications(contract);
        
        var notifications = Notifications
            .Select(n => new DomainExceptionNotification { Key = n.Key, Message = n.Message })
            .ToArray();
        
        if (Notifications.Count != 0)
        {
            throw new DomainException(notifications);
        }
    }

    protected void AddNotifications(ValueObject? valueObject)
    {
        if(valueObject is null) return;
        AddNotifications(valueObject.Notifications);
    }
}