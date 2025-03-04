namespace GeekSevenLabs.AdventEcho.Domain;

public interface IAdventEchoUnitOfWork
{
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}