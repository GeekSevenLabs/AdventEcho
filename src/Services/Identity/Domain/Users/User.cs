namespace AdventEcho.Identity.Domain.Users;

public sealed class User
{
    public Guid Id { get; private set; }
    
    public string? Name { get; private set; }
    public string? Email { get; private set; }
    public string? PasswordHash { get; private set; }
}