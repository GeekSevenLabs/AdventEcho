namespace AdventEcho.Identity.Application.Shared.Accounts.ConfirmEmail;

public class ConfirmEmailAccountValidator : AbstractValidator<ConfirmEmailAccountRequest>
{
    public ConfirmEmailAccountValidator()
    {
        RuleFor(request => request.UserId).NotEmpty();
        RuleFor(request => request.Code).NotEmpty();
    }
}