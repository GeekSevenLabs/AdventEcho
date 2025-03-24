namespace AdventEcho.Identity.Application.Shared.ConfirmEmail;

public class ConfirmUserEmailValidator : AbstractValidator<ConfirmUserEmailRequest>
{
    public ConfirmUserEmailValidator()
    {
        RuleFor(request => request.UserId).NotEmpty();
        RuleFor(request => request.Code).NotEmpty();
    }
}