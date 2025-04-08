namespace AdventEcho.Kernel.Application.Errors;

public static class SecurityErrors
{
    public static IEchoError[] Unauthorized => [new EchoError("Unauthorized").WithErrorCode("SCY0001")];
    public static IEchoError[] Forbidden(string message) => [new EchoError(message).WithErrorCode("SCY0002")];
}