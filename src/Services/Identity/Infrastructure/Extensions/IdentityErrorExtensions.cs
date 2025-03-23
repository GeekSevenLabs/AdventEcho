using AdventEcho.Kernel.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace AdventEcho.Identity.Infrastructure.Extensions;

internal static class IdentityErrorExtensions
{
    public static AdventEchoValidationException ToValidationException(this IEnumerable<IdentityError> errors)
    {
        var errorsDic = errors.ToDictionary(error => error.Code, error => new[] { error.Description });
        return new AdventEchoValidationException(errorsDic);
    }
}