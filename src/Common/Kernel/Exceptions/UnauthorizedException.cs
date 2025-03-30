namespace AdventEcho.Kernel.Exceptions;

public class UnauthorizedException : Exception
{
    public static UnauthorizedException New => new();
}