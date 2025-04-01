namespace AdventEcho.Kernel.Application.Shared.Messages;

public abstract class Message
{
    protected Message()
    {
        MessageType = GetType().Name;
    }
    
    public string MessageType { get; protected set; }
}