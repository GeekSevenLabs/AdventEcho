using GeekSevenLabs.AdventEcho.Application.Services;
using GeekSevenLabs.AdventEcho.Application.Shared;
using Menso.Tools.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace GeekSevenLabs.AdventEcho.Infrastructure.Identity.Services;

internal class UserContext(
    UserManager<ApplicationUser> userManager,
    IHttpContextAccessor httpContextAccessor) : IUserContext
{
    public async Task<bool> CurrentUserCanCreateDistrictAsync()
    {
        var user = httpContextAccessor.HttpContext?.User;
        Throw.Http.Forbidden.When.Null(user, "User not logged in.");
        
        var userAccount = await userManager.GetUserAsync(user);
        Throw.Http.Forbidden.When.Null(userAccount, "User not logged in.");
        
        // Can create district if user is in the Administrator or Developer role
        return  await userManager.IsInRoleAsync(userAccount, StringsRoles.Administrator) ||
                await userManager.IsInRoleAsync(userAccount, StringsRoles.Developer);
    }
}