namespace GeekSevenLabs.AdventEcho.Kernel.Data;

public interface IUnitOfWork
{
    Task<bool> CommitAsync(CancellationToken cancellationToken = default);
}