using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AdventEcho.Identity.Application.Services.Tokens;
using AdventEcho.Identity.Application.Shared.Accounts;
using AdventEcho.Identity.Domain.Users;
using AdventEcho.Kernel.Application.Shared;
using AdventEcho.Kernel.Extensions;
using AdventEcho.Kernel.Messages;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AdventEcho.Identity.Infrastructure.Services;

internal class TokenService(UserManager<User> userManager, IOptions<AdventEchoIdentityJwtConfiguration> options) : ITokenService
{
    private readonly AdventEchoIdentityJwtConfiguration _options = options.Value;
    
    public async Task<Result<JwtTokens>> GenerateTokensAsync(IUser user)
    {
        var currentUser = (User)user;
        var claims = await GetClaims(currentUser);
        var (validIn, expires) = _options.GetAccessTokenLifetime();
        
        var token = new JwtSecurityToken(
            issuer: _options.Issuer,
            audience: _options.Audiences.First(),
            claims: claims,
            expires: expires,
            notBefore: validIn,
            signingCredentials: CreateSigningCredentials());

        var accessToken = new JwtToken
        {
            Value = new JwtSecurityTokenHandler().WriteToken(token),
            Expires = expires,
            ValidIn = validIn
        };
        
        var refreshToken = await GenerateRefreshToken(currentUser, accessToken);
        
        return new JwtTokens(accessToken, refreshToken);
    }

    private async Task<JwtToken> GenerateRefreshToken(IUser user, JwtToken accessToken)
    {
        var currentUser = (User)user;
        var claims = await GetClaims(currentUser, forRefreshTokens: true);
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
    
    private async Task<IEnumerable<Claim>> GetClaims(User user, bool forRefreshTokens = false)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Name.Required()),
            new(AdventEchoClaims.UserId, user.Id.Required().ToString()),
            new(JwtRegisteredClaimNames.Jti, user.Id.Required().ToString())
        };

        if (forRefreshTokens)
        {
            claims.Add(new Claim(AdventEchoClaims.RefreshToken, AdventEchoClaims.RefreshToken));
            return claims;
        }
        
        claims.AddRange([
            new Claim(ClaimTypes.NameIdentifier, user.Name.Required()),
            new Claim(ClaimTypes.Name, user.Name.Required()),
            new Claim(JwtRegisteredClaimNames.Sub, user.Name.Required()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email.Required())
        ]);
        
        var roles = await userManager.GetRolesAsync(user);
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var userClaims = await userManager.GetClaimsAsync(user);
        claims.AddRange(userClaims);

        return claims;
    }
    
}