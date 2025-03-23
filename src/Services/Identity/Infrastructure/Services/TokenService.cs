using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AdventEcho.Identity.Application;
using AdventEcho.Identity.Application.Shared;
using AdventEcho.Kernel.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AdventEcho.Identity.Infrastructure.Services;

internal class TokenService(UserManager<User> userManager, IOptions<AdventEchoIdentityJwtConfiguration> options) : ITokenService
{
    private readonly AdventEchoIdentityJwtConfiguration _options = options.Value;
    
    public async Task<JwtToken> GenerateToken(IUser user)
    {
        var currentUser = (User)user;
        var claims = await GetClaims(currentUser);
        var (validIn, expiresIn) = _options.GetAccessTokenLifetime();
        
        var token = new JwtSecurityToken(
            issuer: _options.Issuer,
            audience: _options.Audiences.First(),
            claims: claims,
            expires: expiresIn,
            notBefore: validIn,
            signingCredentials: CreateSigningCredentials());

        return new JwtToken
        {
            Value = new JwtSecurityTokenHandler().WriteToken(token),
            Expires = expiresIn,
            ValidIn = validIn
        };
    }

    public async Task<JwtToken> GenerateRefreshToken(IUser user, JwtToken accessToken)
    {
        var currentUser = (User)user;
        var claims = await GetClaims(currentUser, addUserClaims: false);
        var (validIn, expiresIn) = _options.GetRefreshTokenLifetime(accessToken.Expires);
        
        var token = new JwtSecurityToken(
            issuer: _options.Issuer,
            audience: _options.Audiences.First(),
            claims: claims,
            expires: expiresIn,
            notBefore: validIn,
            signingCredentials: CreateSigningCredentials());
        
        return new JwtToken
        {
            Value = new JwtSecurityTokenHandler().WriteToken(token),
            Expires = expiresIn,
            ValidIn = validIn
        };
    }

    private SigningCredentials CreateSigningCredentials()
    {
        var config = options.Value;
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.Secret));
        return new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
    }
    
    private async Task<IEnumerable<Claim>> GetClaims(User user, bool addUserClaims = true)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Name.Required()),
            new(JwtRegisteredClaimNames.Sub, user.Name.Required()),
            new(JwtRegisteredClaimNames.Email, user.Email.Required()),
            new(JwtRegisteredClaimNames.Jti, user.Id.Required().ToString())
        };

        if (!addUserClaims) { return claims; }
        
        var roles = await userManager.GetRolesAsync(user);
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var userClaims = await userManager.GetClaimsAsync(user);
        claims.AddRange(userClaims);

        return claims;
    }
}