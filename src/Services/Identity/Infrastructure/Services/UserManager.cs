using System.Web;
using AdventEcho.Identity.Application;
using AdventEcho.Kernel.Messages;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AdventEcho.Identity.Infrastructure.Services;

internal class UserServiceInternal(
    UserManager<User> userManager,
    SignInManager<User> signInManager,
    IUserStore<User> userStore,
    ILogger<UserServiceInternal> logger,
    IOptions<AdventEchoIdentityDomainsConfiguration> options,
    IEmailSender<User> emailSender) : IUserService
{
    public async Task<IUser?> FindByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await userManager.FindByEmailAsync(email); 
    }

    public async Task<Result<IUser>> LoginAsync(IUser user, string password, CancellationToken cancellationToken = default)
    {
        var result = await signInManager.CheckPasswordSignInAsync((User)user, password, false);
        
        if(result.IsLockedOut) return Result.Fail<IUser>("User account is locked out.");
        if(result.IsNotAllowed) return Result.Fail<IUser>("User account is not allowed.");
        if(result.RequiresTwoFactor) return Result.Fail<IUser>("User account requires two factor authentication.");
        
        return Result.Ok(user);
    }

    public async Task<Result> RegisterAsync(string name, string email, string password, CancellationToken cancellationToken = default)
    {
        var user = new User(name);
        var domainConfigs = options.Value;

        await userStore.SetUserNameAsync(user, email, cancellationToken);
        await ((IUserEmailStore<User>)userStore).SetEmailAsync(user, email, cancellationToken);
        
        var result = await userManager.CreateAsync(user, password);

        if (!result.Succeeded)
        {
            var errors = result.Errors.ToDictionary(error => error.Code, error => new[] { error.Description });
            return Result.Fail(errors);
        }
        
        logger.LogInformation("User account created for {email} and password set.", email);
        
        var userId = await userManager.GetUserIdAsync(user);
        var code = await userManager.GenerateEmailConfirmationTokenAsync(user);
        
        code = HttpUtility.UrlEncode(code);
        
        var callbackUrl = $"{domainConfigs.ApiIdentity}/account/confirm-email?userId={userId}&code={code}";
        
        await emailSender.SendConfirmationLinkAsync(user, email, callbackUrl);
        
        return Result.Ok();
    }
}