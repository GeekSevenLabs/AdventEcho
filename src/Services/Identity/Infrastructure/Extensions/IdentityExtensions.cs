using Microsoft.AspNetCore.Identity;

namespace AdventEcho.Identity.Infrastructure.Extensions;

public static class IdentityExtensions
{
    public static IEchoError[] ToResultErrors(this IEnumerable<IdentityError> errors)
    {
        return errors
            .Select(e => new EchoError(e.Description).WithErrorCode(e.Code))
            .ToArray<IEchoError>();
    }
}