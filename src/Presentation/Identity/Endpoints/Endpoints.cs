namespace AdventEcho.Presentation.Identity.Endpoints;

public static class Endpoints
{
    public static void MapAdventEchoIdentityVersionOneEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/api/v1");
            
        group.MapAccountEndpoints();
    }
}