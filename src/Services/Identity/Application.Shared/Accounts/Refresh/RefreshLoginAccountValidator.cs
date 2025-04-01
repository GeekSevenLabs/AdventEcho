namespace AdventEcho.Identity.Application.Shared.Accounts.Refresh;

public class RefreshLoginAccountValidator : AbstractValidator<RefreshLoginAccountRequest>
{
    public RefreshLoginAccountValidator()
    {
        RuleFor(request => request.RefreshToken).NotEmpty();
    }
}