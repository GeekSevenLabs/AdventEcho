// ReSharper disable once CheckNamespace
namespace AdventEcho;

public abstract class Entity
{
    public Guid Id { get; protected set; }
    
    public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; protected set; } = DateTime.UtcNow;
}