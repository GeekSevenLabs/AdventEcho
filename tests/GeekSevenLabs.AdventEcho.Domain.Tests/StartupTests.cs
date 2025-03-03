using System.Runtime.CompilerServices;
using Menso.Tools.Exceptions;

namespace GeekSevenLabs.AdventEcho.Domain.Tests;

public static class StartupTests
{
    [ModuleInitializer]
    public static void Prepare()
    {
        ExceptionSettings.CreateDefaultExceptionHandle = exceptionInformation =>
        {
            var message = exceptionInformation.CustomMessage ?? exceptionInformation.DefaultMessage;
            return new InvalidOperationException(message, exceptionInformation.InnerException);
        };
    }
}