// ReSharper disable once CheckNamespace
namespace AdventEcho;

public interface IExceptionalError : IEchoError
{
    Exception Exception { get; }
}