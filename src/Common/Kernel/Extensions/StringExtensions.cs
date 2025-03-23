using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Menso.Tools.Exceptions;

namespace AdventEcho.Kernel.Extensions;

public static class StringExtensions
{
    public static string Required([NotNull] this string? value, [CallerArgumentExpression("value")] string? paramName = null)
    {
        Throw.When.Null(value, $"{paramName} is required");
        return value;
    }

    public static InvalidOperationException ToInvalidOperationException(this string message)
    {
        return new InvalidOperationException(message);
    }

    public static ArgumentNullException ToArgumentNullException(this string message)
    {
        return new ArgumentNullException(message, (Exception?)null);
    }
    
}