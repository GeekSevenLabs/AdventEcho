// ReSharper disable once CheckNamespace
namespace AdventEcho;

public class EchoExceptionalError : EchoError
{
    #region Constructors

    public EchoExceptionalError(Exception exception)
    {
        Exception = exception;
    }
    
    public EchoExceptionalError(string message, Exception exception) : base(message)
    {
        Exception = exception;
    }

    #endregion

    #region Properties

    public Exception Exception { get; }

    #endregion
}