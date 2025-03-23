namespace AdventEcho.Identity.Application;

public interface IUser
{
    Guid Id { get; }
    string? Name { get; }
    string? Email { get; }
}