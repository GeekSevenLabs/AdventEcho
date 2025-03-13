namespace GeekSevenLabs.AdventEcho.Application.Shared.Districts;

public class CreateDistrictRequestValidator : AbstractValidator<CreateDistrictRequest>
{
    public CreateDistrictRequestValidator()
    {
        RuleFor(district => district.Name).NotEmpty().MaximumLength(100);
        RuleFor(district => district.PastorId).NotEmpty();
    }
}