using GeekSevenLabs.AdventEcho.Application.Districts.Create;
using GeekSevenLabs.AdventEcho.Domain.Shared;
using GeekSevenLabs.AdventEcho.Kernel.Communication.Mediator;
using CreateDistrictRequest = GeekSevenLabs.AdventEcho.Application.Shared.Districts.CreateDistrictRequest;

namespace GeekSevenLabs.AdventEcho.Presentation.Web.Endpoints.Districts;

public static class CreateDistrictEndpoint
{
    public static async Task<DistrictId> ExecuteAsync(IMediatorHandler mediator, CreateDistrictRequest request)
    {
        var result = await mediator.SendCommand((CreateDistrictCommand)request);
        return result.Value;
    }
}