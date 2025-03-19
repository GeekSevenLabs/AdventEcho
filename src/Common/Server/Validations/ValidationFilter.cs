using FluentValidation;
using FluentValidation.Results;
using Menso.Tools.Exceptions;
using Microsoft.AspNetCore.Http;

namespace AdventEcho.Kernel.Server.Validations;

public class ValidationFilter<TModel>(IValidator<TModel> validator) : IEndpointFilter where TModel : class
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var parameter = context.Arguments.SingleOrDefault(p => p?.GetType() == typeof(TModel));
        Throw.When.Null(parameter, $"[{GetType().FullName}] :: Parameter not found for type {typeof(TModel).FullName}");

        var validationResult = await validator.ValidateAsync((TModel)parameter);

        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(ValidationResultToProblemDetails(validationResult));
        }
        
        return await next(context);
    }

    private static Dictionary<string, string[]> ValidationResultToProblemDetails(ValidationResult validationResult)
    {
        return validationResult
            .Errors
            .Select(e => new { e.PropertyName, e.ErrorMessage })
            .GroupBy(e => e.PropertyName, e => e.ErrorMessage )
            .ToDictionary(e => e.Key, e => e.ToArray());
    }
}