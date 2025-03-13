using GeekSevenLabs.AdventEcho.Kernel.Messages;

namespace GeekSevenLabs.AdventEcho.Kernel;

public abstract class Entity<TKey> where TKey : struct
{
    private readonly List<Event> _events = [];
    
    public TKey Id { get; protected set; }
    public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; protected set; }
    
    public IReadOnlyCollection<Event> Events => _events.AsReadOnly();
    
    public void AddEvent(Event eventItem) => _events.Add(eventItem);
    public void RemoveEvent(Event eventItem) => _events.Remove(eventItem);
    public void ClearEvents() => _events.Clear();
    
    public void Update() => UpdatedAt = DateTime.UtcNow;
    
    public override bool Equals(object? obj)
    {
        var compareTo = obj as Entity<TKey>;
        
        if(ReferenceEquals(this, compareTo)) return true;
        
        return !ReferenceEquals(null, compareTo) && Id.Equals(compareTo.Id);
    }
    
    public static bool operator ==(Entity<TKey> a, Entity<TKey> b)
    {
        if(ReferenceEquals(a, null) && ReferenceEquals(b, null))
        {
            return true;
        }
        
        if(ReferenceEquals(a, null) || ReferenceEquals(b, null))
        {
            return false;
        }
        
        return a.Equals(b);
    }
    
    public static bool operator !=(Entity<TKey> a, Entity<TKey> b) => !(a == b);
    
    public override int GetHashCode() => Id.GetHashCode();
    
    public override string ToString() => GetType().Name + " [Id=" + Id + "]";
}