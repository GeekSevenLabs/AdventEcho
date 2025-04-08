// ReSharper disable once CheckNamespace
namespace AdventEcho;

public interface IEchoReason
{
     string Message { get; }
     Dictionary<string, object> Metadata { get; }
}