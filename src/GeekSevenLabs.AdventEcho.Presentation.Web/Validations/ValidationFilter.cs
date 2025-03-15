using FluentValidation;
using FluentValidation.Results;
using Menso.Tools.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace GeekSevenLabs.AdventEcho.Presentation.Web.Validations;

public class ValidationFilter<TModel>(IValidator<TModel> validator) : IEndpointFilter where TModel : class
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var parameter = context.Arguments.SingleOrDefault(p => p?.GetType() == typeof(TModel));
        Throw.When.Null(parameter, $"[{GetType().FullName}] :: Parameter not found for type {typeof(TModel).FullName}");

        var validationResult = await validator.ValidateAsync((TModel)parameter);

        if (!validationResult.IsValid)
        {
            return Results.Problem(ValidationResultToProblemDetails(validationResult));
        }

        // now the actual endpoint execution
        return await next(context);
    }

    private static ProblemDetails ValidationResultToProblemDetails(ValidationResult validationResult)
    {
        var problemDetails = new ProblemDetails
        {
            Title = "Validation error",
            Status = 400,
            Detail = "One or more validation errors occurred.",
            Instance = "urn:geeksevenlabs:adventecho:validation",
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
        };

        foreach (var error in validationResult.Errors)
        {
            problemDetails.Extensions.Add(error.PropertyName, error.ErrorMessage);
        }

        return problemDetails;
    }
}