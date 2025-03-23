using AdventEcho.Identity.Application.Shared;

namespace AdventEcho.Identity.Application;

public interface ITokenService
{
    Task<JwtToken> GenerateToken(IUser user);
    Task<JwtToken> GenerateRefreshToken(IUser user, JwtToken accessToken);
}