namespace AdventEcho.Identity.Application.Shared.Accounts.Register;

public class RegisterUserRequest
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
}