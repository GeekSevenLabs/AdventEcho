// ReSharper disable once CheckNamespace
namespace AdventEcho;

public class EchoError : IEchoError
{
    #region Constructors

    protected EchoError()
    {
        Message = string.Empty;
        Metadata = [];
    }
    
    public EchoError(string message) : this() => Message = message;

    #endregion
    
    #region Properties
    
    public string Message { get; }
    public Dictionary<string, object> Metadata { get; }
    
    #endregion

    #region Modifiers behaviors

    public EchoError WithMetadata(string key, object value)
    {
        Metadata[key] = value;
        return this;
    }
    
    public EchoError WithErrorCode(string code)
    {
        Metadata[ResultConstants.Metadata.ErrorCode] = code;
        return this;
    }

    #endregion
}