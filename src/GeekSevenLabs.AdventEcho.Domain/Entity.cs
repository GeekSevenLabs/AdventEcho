using Flunt.Notifications;

namespace GeekSevenLabs.AdventEcho.Domain;

public abstract class Entity<TEntity, TKey> : Notifiable<Notification>, IEqualityComparer<Entity<TEntity, TKey>> where TKey : struct
{ 
    public TKey Id { get; protected set; }
    
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
    
    public bool Equals(Entity<TEntity, TKey>? x, Entity<TEntity, TKey>? y)
    {
        if(x is null && y is null)
        {
            return true;
        }
        
        if(x is null || y is null)
        {
            return false;
        }
        
        return x.Id.Equals(y.Id);
    }

    public int GetHashCode(Entity<TEntity, TKey> obj)
    {
        return obj.Id.GetHashCode();
    }
}