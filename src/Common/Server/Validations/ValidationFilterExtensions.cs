using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace AdventEcho.Kernel.Server.Validations;

public static class ValidationFilterExtensions
{
    public static RouteHandlerBuilder UseValidationFor<TModel>(this RouteHandlerBuilder builder) where TModel : class
    {
        return builder.AddEndpointFilter<ValidationFilter<TModel>>();
    }
}