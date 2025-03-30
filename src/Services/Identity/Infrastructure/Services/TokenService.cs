using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AdventEcho.Identity.Application;
using AdventEcho.Identity.Application.Services.Tokens;
using AdventEcho.Identity.Application.Shared.Accounts;
using AdventEcho.Kernel.Application.Shared;
using AdventEcho.Kernel.Extensions;
using AdventEcho.Kernel.Messages;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AdventEcho.Identity.Infrastructure.Services;

internal class TokenService(
    SigningCredentials signingCredentials,
    UserManager<User> userManager,
    IOptions<AdventEchoIdentityOption> options) : ITokenService
{
    private readonly AdventEchoIdentityOption _options = options.Value;
    
    public async Task<Result<JwtTokens>> GenerateTokensAsync(IUser user)
    {
        var currentUser = (User)user;
        var claims = await GetClaimsAsync(currentUser);
        var expires = _options.Jwt.GetAccessTokenLifetime();
        
        var token = new JwtSecurityToken(
            issuer: _options.Jwt.Issuer,
            audience: _options.Jwt.Audiences.First(),
            claims: claims,
            expires: expires,
            signingCredentials: signingCredentials);

        var accessToken = new AccessToken
        {
            Value = new JwtSecurityTokenHandler().WriteToken(token),
            Expires = expires
        };
        
        var refreshToken = GenerateRefreshToken(currentUser);
        
        return new JwtTokens(accessToken, refreshToken);
    }

    private RefreshToken GenerateRefreshToken(User user)
    {
        var expiresIn = _options.Jwt.GetRefreshTokenLifetime();
        return new RefreshToken(user.Id, user.Email!, expiresIn);
    }
    
    private async Task<IEnumerable<Claim>> GetClaimsAsync(User user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Name.Required()),
            new(ClaimTypes.Name, user.Name.Required()),
            new(AdventEchoClaims.UserId, user.Id.Required().ToString()),
            new(JwtRegisteredClaimNames.Jti, user.Id.Required().ToString()),
            new(JwtRegisteredClaimNames.Email, user.Email.Required()),
            new Claim(JwtRegisteredClaimNames.Sub, user.Name.Required())
        };
        
        var roles = await userManager.GetRolesAsync(user);
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var userClaims = await userManager.GetClaimsAsync(user);
        claims.AddRange(userClaims);

        return claims;
    }
    
}