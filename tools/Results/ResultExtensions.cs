namespace AdventEcho;

public static class ResultExtensions
{
    public static bool HasException<TException>(this IEnumerable<IEchoError> errors)
    {
        return errors
            .OfType<IExceptionalError>()
            .Any(ee => ee.Exception is TException);
    }
    
    public static bool TryGetException<TException>(this IEnumerable<IEchoError> errors, out TException? exception) where TException : Exception
    {
        var ex = errors
            .OfType<IExceptionalError>()
            .Select(ee => ee.Exception)
            .OfType<TException>()
            .FirstOrDefault();

        if (ex is not null)
        {
            exception = ex;
            return true;
        }

        exception = null;
        return false;
    }
}