namespace AdventEcho.Identity.Application.Shared.Accounts.Register;

public class RegisterAccountValidator : AbstractValidator<RegisterAccountRequest>
{
    public RegisterAccountValidator()
    {
        RuleFor(request => request.Email).EmailAddress();
        
        RuleFor(request => request.Password)
            .MinimumLength(8)
            .MaximumLength(120)
            .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
            .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
            .Matches("[0-9]").WithMessage("Password must contain at least one number.")
            .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character.");
        
        RuleFor(request => request.FirstName)
            .NotEmpty()
            .MaximumLength(50)
            .Matches("^[a-zA-Z]+$").WithMessage("First name can only contain letters.");
        
        RuleFor(request => request.LastName)
            .NotEmpty()
            .MaximumLength(50)
            .Matches("^[a-zA-Z]+$").WithMessage("Last name can only contain letters.");
    }
}