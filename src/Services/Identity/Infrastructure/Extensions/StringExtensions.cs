using Microsoft.AspNetCore.Identity;

namespace AdventEcho.Identity.Infrastructure.Extensions;

public static class StringExtensions
{
    public static Dictionary<string, string[]> ToDictionary(this IEnumerable<IdentityError> errors)
    {
        return errors.ToDictionary(error => error.Code, error => new[] { error.Description });
    }
}