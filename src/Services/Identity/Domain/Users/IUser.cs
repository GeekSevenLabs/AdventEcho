namespace AdventEcho.Identity.Domain.Users;

public interface IUser
{
    Guid Id { get; }
    string? Name { get; }
    string? Email { get; }
}