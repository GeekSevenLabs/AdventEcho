using Flunt.Notifications;

namespace GeekSevenLabs.AdventEcho.Domain;

public sealed class DomainException(DomainExceptionNotification[] notifications) : Exception
{
    public DomainExceptionNotification[] Notifications { get; init; } = notifications;
}

public sealed class DomainExceptionNotification : Notification;