using AdventEcho.Identity.Application.Shared;
using AdventEcho.Kernel.Application.Shared;
using AdventEcho.Kernel.Server.Endpoints;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace AdventEcho.Kernel.Server.Extensions;

public static class EndpointExtensions
{
    public static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder endpoints) where TEndpoint : IEndpoint
    {
        return TEndpoint.Map(endpoints);
    }
    
    public static RouteHandlerBuilder WithDefinition(this RouteHandlerBuilder builder, EndpointDefinition definition)
    {
        return builder
            .WithName(definition.Name)
            .WithDescription(definition.Description)
            .WithTags(definition.Tag);
    }
    
    public static RouteHandlerBuilder ProducesCommonResponse<TResponse>(this RouteHandlerBuilder builder)
    {
        return builder
            .ProducesCommonResponse()
            .Produces<TResponse>();
    }
    
    public static RouteHandlerBuilder ProducesCommonResponse(this RouteHandlerBuilder builder)
    {
        return builder
            .ProducesValidationProblem()
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);
    }
}