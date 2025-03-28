using System.Security.Claims;
using AdventEcho.Kernel.Extensions;
using AdventEcho.Kernel.Messages;

namespace AdventEcho.Kernel.Application.Shared.Extensions;

public static class UserExtensions
{
    public static Result<Guid> GetUserId(this ClaimsPrincipal principal)
    {
        var userIdString = principal.FindClaim(AdventEchoClaims.UserId);
        if (string.IsNullOrWhiteSpace(userIdString)) return Result<Guid>.Fail("Invalid operation".ToInvalidOperationException());
        
        return Guid.TryParse(userIdString, out var userId) ? 
            Result<Guid>.Success(userId) : 
            Result<Guid>.Fail("Invalid operation".ToInvalidOperationException());
    }

    private static string? FindClaim(this ClaimsPrincipal principal, string claimType)
    {
        return principal.Claims.FirstOrDefault(c => c.Type == claimType)?.Value;
    }
}