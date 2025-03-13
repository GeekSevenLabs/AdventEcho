namespace GeekSevenLabs.AdventEcho.Kernel.Data;

public interface IRepository<T> : IDisposable where T : IAggregateRoot
{
    IUnitOfWork UnitOfWork { get; }
}