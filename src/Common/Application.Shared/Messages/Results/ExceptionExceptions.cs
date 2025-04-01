using AdventEcho.Kernel.Exceptions;

namespace AdventEcho.Kernel.Application.Shared.Messages.Results;

public static class EchoResults
{
    public static Result Success() => Result.Success();
    public static Result Fail(Exception error) => Result.Fail(error);
    
    public static Result NotFound(string message) => new NotFoundException(message);
    public static Result Forbidden(string message) => new ForbiddenException(message);
    public static Result BadRequest(string message) => new BadRequestException(message);
    public static Result Unauthorized(string message = nameof(Unauthorized)) => new UnauthorizedException(message);
    public static Result Conflict(string message) => new ConflictException(message);
    
    public static Result Validation(IDictionary<string, string[]> problems) => new ValidationException("", problems);
    public static Result Validation(string message, IDictionary<string, string[]> problems) => new ValidationException(message, problems);
    public static Result Validation(string message, string field, string[] problems) => new ValidationException(message, new Dictionary<string, string[]> { [field] = problems });
}

public static class EchoResults<T>
{
    public static Result<T> Success(T value) => Result<T>.Success(value);
    public static Result<T> Fail(Exception error) => Result<T>.Fail(error);
    
    
    public static Result<T> NotFound(string message) => Result<T>.Fail(new NotFoundException(message));
    public static Result<T> Forbidden(string message) => new ForbiddenException(message);
    public static Result<T> BadRequest(string message) => new BadRequestException(message);
    public static Result<T> Unauthorized(string message = nameof(Unauthorized)) => new UnauthorizedException(message);
    public static Result<T> Conflict(string message) => new ConflictException(message);
    
    public static Result<T> Validation(string message, IDictionary<string, string[]> problems) => new ValidationException(message, problems);
    public static Result<T> Validation(string message, string field, string[] problems) => new ValidationException(message, new Dictionary<string, string[]> { [field] = problems });
}