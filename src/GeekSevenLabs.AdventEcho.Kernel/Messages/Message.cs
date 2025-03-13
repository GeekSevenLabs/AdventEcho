namespace GeekSevenLabs.AdventEcho.Kernel.Messages;

public abstract class Message
{
    protected Message()
    {
        MessageType = GetType().Name;
    }
    
    public string MessageType { get; protected set; }
}