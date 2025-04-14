namespace AdventEcho.Identity.Application.Shared.Accounts.Login;

public class LoginAccountValidator : AbstractValidator<LoginAccountRequest>
{
    public LoginAccountValidator()
    {
        RuleFor(request => request.Email).NotEmpty().EmailAddress();
        RuleFor(request => request.Password).NotEmpty();
    }
}