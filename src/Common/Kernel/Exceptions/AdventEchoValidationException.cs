namespace AdventEcho.Kernel.Exceptions;

public class AdventEchoValidationException(Dictionary<string, string[]> problems) : Exception("One or more validation errors occurred.")
{
    public Dictionary<string, string[]> Problems { get; private set; } = problems;
}