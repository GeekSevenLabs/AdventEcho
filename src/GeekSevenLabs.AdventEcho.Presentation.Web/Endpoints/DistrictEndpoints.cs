using GeekSevenLabs.AdventEcho.Application.Districts.Create;
using GeekSevenLabs.AdventEcho.Application.Districts.Get;
using GeekSevenLabs.AdventEcho.Application.Districts.Update;
using GeekSevenLabs.AdventEcho.Application.Shared.Districts.Editor;
using GeekSevenLabs.AdventEcho.Domain.Shared;
using GeekSevenLabs.AdventEcho.Kernel.Communication.Mediator;
using GeekSevenLabs.AdventEcho.Presentation.Web.Validations;

namespace GeekSevenLabs.AdventEcho.Presentation.Web.Endpoints;

public static class DistrictEndpoints
{
    private static async Task<IResult> CreateDistrict(IMediatorHandler mediator, EditorDistrictRequest request)
    {
        return (await mediator.SendCommand((CreateDistrictCommand)request)).ProcessResult();
    }
    
    private static async Task<IResult> UpdateDistrict(IMediatorHandler mediator, EditorDistrictRequest request, DistrictId id)
    {
        request.Id = id;
        return (await mediator.SendCommand((UpdateDistrictCommand)request)).ProcessResult();
    }
    
    private static async Task<IResult> GetDistrict(IMediatorHandler mediator, DistrictId id)
    {
        var query = new GetDistrictQuery { Id = id };
        return (await mediator.SendQuery(query)).ProcessResult();
    }
    
    public static void MapDistrictEndpoints(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("/districts").RequireAuthorization();

        group
            .MapPost("", CreateDistrict)
            .AddValidation<EditorDistrictRequest>();
        
        group
            .MapPut("{id}", UpdateDistrict)
            .AddValidation<EditorDistrictRequest>();
        
        group
            .MapGet("{id}", GetDistrict);
    }
}