using GeekSevenLabs.AdventEcho.Application.Shared.Districts;
namespace GeekSevenLabs.AdventEcho.Application.Districts.Create;
public sealed class CreateDistrictCommand : CreateDistrictRequest, ICommand<DistrictId>;