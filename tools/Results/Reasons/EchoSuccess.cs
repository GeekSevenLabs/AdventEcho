// ReSharper disable once CheckNamespace

namespace AdventEcho;

public class EchoSuccess : IEchoSuccess
{
    #region Constructors

    public EchoSuccess()
    {
        Message = string.Empty;
        Metadata = [];
    }

    public EchoSuccess(string message)
    {
        Message = message;
        Metadata = [];
    }

    #endregion

    #region Static Factory Methods

    public static EchoSuccess Ok() => new();
    public static EchoSuccess Ok(string message) => new(message);

    #endregion

    #region Properties

    public string Message { get; }
    public Dictionary<string, object> Metadata { get; }

    #endregion

    #region Modifiers Behavior

    public IEchoSuccess WithMetadata(string key, object value)
    {
        Metadata.Add(key, value);
        return this;
    }

    #endregion
}