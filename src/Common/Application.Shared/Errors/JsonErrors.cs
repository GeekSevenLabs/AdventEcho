namespace AdventEcho.Kernel.Application.Shared.Errors;

public static class JsonErrors
{
    public static IEchoError[] InvalidJson<TResponse>() => [new EchoError($"Json content is null or not valid for this type {typeof(TResponse).Name}").WithErrorCode("JSON001")];
}