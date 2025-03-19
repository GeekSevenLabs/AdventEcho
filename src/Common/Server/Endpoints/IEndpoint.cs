using Microsoft.AspNetCore.Routing;

namespace AdventEcho.Kernel.Server.Endpoints;

public interface IEndpoint
{
    static abstract IEndpointRouteBuilder Map(IEndpointRouteBuilder endpoints);
}