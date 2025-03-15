using GeekSevenLabs.AdventEcho.Application.Shared.Districts.Editor;

namespace GeekSevenLabs.AdventEcho.Application.Districts.Create;
public sealed class CreateDistrictCommand : EditorDistrictRequest, ICommand<DistrictId>;