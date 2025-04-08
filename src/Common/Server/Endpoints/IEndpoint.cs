using Microsoft.AspNetCore.Routing;

// ReSharper disable once CheckNamespace
namespace AdventEcho;

public interface IEndpoint
{
    static abstract IEndpointRouteBuilder Map(IEndpointRouteBuilder endpoints);
}