using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Menso.Tools.Exceptions;

namespace AdventEcho.Kernel.Extensions;

public static class ObjectExtensions
{
    public static T Required<T>([NotNull] this T? value, [CallerArgumentExpression("value")] string? paramName = null) where T : class
    {
        Throw.When.Null(value, $"{paramName} is required");
        return value;
    }
    
    public static Guid Required([NotNull] this Guid? value, [CallerArgumentExpression("value")] string? paramName = null)
    {
        Throw.When.NullOrEmpty(value, $"{paramName} is required");
        return value.Value;
    }
    
    public static Guid Required(this Guid value, [CallerArgumentExpression("value")] string? paramName = null)
    {
        Throw.When.NullOrEmpty(value, $"{paramName} is required");
        return value;
    }
}