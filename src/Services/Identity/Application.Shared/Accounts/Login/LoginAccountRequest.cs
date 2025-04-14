namespace AdventEcho.Identity.Application.Shared.Accounts.Login;

public class LoginAccountRequest : ICommand<LoginAccountResponse>
{
    public string? Email { get; set; }
    public string? Password { get; set; }
    public bool UseCookie { get; set; }
}