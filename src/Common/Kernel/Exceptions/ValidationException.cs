// ReSharper disable once CheckNamespace
namespace AdventEcho;

public class ValidationException(string message, IDictionary<string, string[]> problems) : Exception(message)
{
    public IDictionary<string, string[]> Problems { get; } = problems;
}