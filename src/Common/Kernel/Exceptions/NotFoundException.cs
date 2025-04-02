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


public class ProblemDetailsException(
    string type,
    string title,
    int status,
    string? detail,
    Dictionary<string, string[]>? errors) : Exception(detail ?? title)
{
    public string Type { get; private set; } = type;
    public string Title { get; private set; } = title;
    public int Status { get; private set; } = status;
    public string Detail { get; private set; } = detail ?? "";
    public Dictionary<string, string[]> Errors { get; private set; } = errors ?? [];
    
    public bool IsUnauthorized => Status == 401;
}


