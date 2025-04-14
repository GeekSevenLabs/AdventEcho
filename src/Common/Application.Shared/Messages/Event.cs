// ReSharper disable once CheckNamespace
namespace AdventEcho;

public abstract class Event : Message, INotification;

public abstract class IntegrationEvent : Event;
public abstract class DomainEvent : Event;