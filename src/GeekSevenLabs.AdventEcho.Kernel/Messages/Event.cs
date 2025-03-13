namespace GeekSevenLabs.AdventEcho.Kernel.Messages;

public abstract class Event : Message, INotification;

public abstract class IntegrationEvent : Event;
public abstract class DomainEvent : Event;