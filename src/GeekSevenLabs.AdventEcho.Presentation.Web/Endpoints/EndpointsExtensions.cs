namespace GeekSevenLabs.AdventEcho.Presentation.Web.Endpoints;

public static class EndpointsExtensions
{
    public static void MapAdventEchoEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapDistrictEndpoints();
    }
}