namespace AdventEcho.Kernel.Exceptions;

public class NotFoundException(string message) : Exception(message);
public class ForbiddenException(string message) : Exception(message);
public class BadRequestException(string message) : Exception(message);
public class UnauthorizedException(string message) : Exception(message);
public class ConflictException(string message) : Exception(message);

public class ValidationException(string message, IDictionary<string, string[]> problems) : Exception(message)
{
    public IDictionary<string, string[]> Problems { get; } = problems;
}



