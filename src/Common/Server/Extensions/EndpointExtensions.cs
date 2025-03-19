using AdventEcho.Kernel.Server.Endpoints;
using Microsoft.AspNetCore.Routing;

namespace AdventEcho.Kernel.Server.Extensions;

public static class EndpointExtensions
{
    public static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder endpoints) where TEndpoint : IEndpoint
    {
        return TEndpoint.Map(endpoints);
    }
}