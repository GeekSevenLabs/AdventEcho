namespace GeekSevenLabs.AdventEcho.Application.Shared.Districts.Editor;

public class EditorDistrictValidator : AbstractValidator<EditorDistrictRequest>
{
    public EditorDistrictValidator()
    {
        RuleFor(district => district.Name).NotEmpty().MaximumLength(100);
        RuleFor(district => district.PastorId).NotEmpty();
    }
}