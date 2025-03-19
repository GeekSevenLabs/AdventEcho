using System.Web;
using AdventEcho.Identity.Application;
using AdventEcho.Kernel.Messages;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AdventEcho.Identity.Infrastructure.Services;

internal class UserManagerInternal(
    UserManager<User> userManager, 
    IUserStore<User> userStore,
    ILogger<UserManagerInternal> logger,
    IOptions<AdventEchoIdentityDomainsConfiguration> options,
    IEmailSender<User> emailSender) : IUserManager
{
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
            return Result.Failure(errors);
        }
        
        logger.LogInformation("User account created for {email} and password set.", email);
        
        var userId = await userManager.GetUserIdAsync(user);
        var code = await userManager.GenerateEmailConfirmationTokenAsync(user);
        
        code = HttpUtility.UrlEncode(code);
        
        var callbackUrl = $"{domainConfigs.ApiIdentity}/account/confirm-email?userId={userId}&code={code}";
        
        await emailSender.SendConfirmationLinkAsync(user, email, callbackUrl);
        
        return Result.Success();
    }
}