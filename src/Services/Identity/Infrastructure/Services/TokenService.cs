using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AdventEcho.Identity.Application.Services.Tokens;
using AdventEcho.Identity.Domain.Users;
using AdventEcho.Kernel.Application.Errors;
using AdventEcho.Kernel.Application.Shared;
using AdventEcho.Kernel.Infrastructure.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AdventEcho.Identity.Infrastructure.Services;

internal class BearerTokenService(
    SigningCredentials signingCredentials,
    UserManager<AdventEchoUser> userManager,
    IOptions<AdventEchoIdentityOptions> options) : IBearerTokenService
{
    private readonly AdventEchoIdentityOptions _options = options.Value;
    
    public async Task<Result<BearerTokens>> GenerateTokensAsync(User user)
    {
        var currentUser = await userManager.FindByIdAsync(user.Id.ToString());
        if (currentUser is null) return SecurityErrors.Unauthorized;
        
        var claims = await GetClaimsAsync(currentUser, user);
        var expiration = DateTimeOffset.UtcNow.AddMinutes(_options.Bearer.AccessTokenExpirationInMinutes);
        
        var token = new JwtSecurityToken(
            issuer: _options.Bearer.Issuer,
            audience: _options.Bearer.Audiences.First(),
            claims: claims,
            expires: expiration.UtcDateTime,
            signingCredentials: signingCredentials
        );
        
        // Add additional audiences 
        foreach (var audience in _options.Bearer.Audiences.Skip(1))
        {
            token.Payload.Add("aud", audience);
        }

        var accessToken = new BearerAccessToken
        {
            Value = new JwtSecurityTokenHandler().WriteToken(token),
            Expiration = expiration
        };
        
        var refreshToken = GenerateRefreshToken(currentUser);
        
        return new BearerTokens(accessToken, refreshToken);
    }

    private BearerRefreshToken GenerateRefreshToken(AdventEchoUser user)
    {
        var expiration = DateTimeOffset.UtcNow.AddMinutes(_options.Bearer.RefreshTokenExpirationInMinutes);
        return new BearerRefreshToken(user.Id, expiration);
    }
    
    private async Task<IEnumerable<Claim>> GetClaimsAsync(AdventEchoUser user, User domainUser)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, domainUser.Name.First),
            new(ClaimTypes.Name, user.Name),
            new(JwtRegisteredClaimNames.Jti, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Email, domainUser.Email),
            new(JwtRegisteredClaimNames.Sub, user.Name)
        };
        
        var roles = await userManager.GetRolesAsync(user);
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var userClaims = await userManager.GetClaimsAsync(user);
        claims.AddRange(userClaims);

        return claims;
    }
    
}