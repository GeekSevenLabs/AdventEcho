namespace AdventEcho.Identity.Application;

public interface ITokenService
{
    Task<string> GenerateToken(IUser user);
}