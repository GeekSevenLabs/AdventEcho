using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AdventEcho.Identity.Application;
using AdventEcho.Kernel.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AdventEcho.Identity.Infrastructure.Services;

internal class TokenService(UserManager<User> userManager, IOptions<AdventEchoIdentityJwtConfiguration> options) : ITokenService
{
    public async Task<string> GenerateToken(IUser user)
    {
        var currentUser = (User)user;
        var config = options.Value;
        
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, user.Name.Required()),
            new(JwtRegisteredClaimNames.Sub, user.Name.Required()),
            new(JwtRegisteredClaimNames.Email, user.Email.Required()),
            new(JwtRegisteredClaimNames.Jti, user.Id.Required().ToString())
        };

        var roles = await userManager.GetRolesAsync(currentUser);
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var userClaims = await userManager.GetClaimsAsync(currentUser);
        claims.AddRange(userClaims);

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.Secret));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: config.Issuer,
            audience: config.Audiences.First(),
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}