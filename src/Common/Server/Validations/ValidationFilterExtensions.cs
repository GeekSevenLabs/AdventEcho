using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

// ReSharper disable once CheckNamespace
namespace AdventEcho;

public static class ValidationFilterExtensions
{
    public static RouteHandlerBuilder UseValidationFor<TModel>(this RouteHandlerBuilder builder) where TModel : class
    {
        return builder.AddEndpointFilter<ValidationFilter<TModel>>();
    }
}