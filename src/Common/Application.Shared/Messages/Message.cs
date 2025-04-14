// ReSharper disable once CheckNamespace
namespace AdventEcho;

public abstract class Message
{
    protected Message()
    {
        MessageType = GetType().Name;
    }
    
    public string MessageType { get; protected set; }
}