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
    
}