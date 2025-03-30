using System.Web;
using AdventEcho.Identity.Application;
using AdventEcho.Identity.Application.Services.Users;
using AdventEcho.Identity.Infrastructure.Extensions;
using AdventEcho.Kernel.Extensions;
using AdventEcho.Kernel.Infrastructure.Options;
using AdventEcho.Kernel.Messages;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AdventEcho.Identity.Infrastructure.Services;

internal class UserService(
    UserManager<User> userManager,
    SignInManager<User> signInManager,
    IUserStore<User> userStore,
    ILogger<UserService> logger,
    IOptions<AdventEchoIdentityDomainsOption> options,
    IEmailSender<User> emailSender) : IUserService
{
    private readonly AdventEchoIdentityDomainsOption _domainConfigs = options.Value;
    
    
    public async Task<Result> RegisterAsync(string name, string email, string password, CancellationToken cancellationToken = default)
    {
        var user = new User(name);
        
        await userStore.SetUserNameAsync(user, email, cancellationToken);
        await ((IUserEmailStore<User>)userStore).SetEmailAsync(user, email, cancellationToken);
        
        var result = await userManager.CreateAsync(user, password);

        if (!result.Succeeded)
        {
            return result.Errors.ToValidationException();
        }
        
        logger.LogInformation("User account created for {email} and password set.", email);
        
        var userId = await userManager.GetUserIdAsync(user);
        var code = await userManager.GenerateEmailConfirmationTokenAsync(user);
        
        code = HttpUtility.UrlEncode(code);
        
        var callbackUrl = $"{_domainConfigs.Web}/account/confirm-email?userId={userId}&code={code}";
        
        await emailSender.SendConfirmationLinkAsync(user, email, callbackUrl);
        
        return Result.Success();
    }

    public async Task<Result> ConfirmEmailAsync(Guid userId, string token, CancellationToken cancellationToken = default)
    {
        var user = await userManager.FindByIdAsync(userId.ToString());
        
        if (user is null) return "This operation is invalid.".ToInvalidOperationException();
        
        var result = await userManager.ConfirmEmailAsync(user, token);
        
        if (!result.Succeeded)
        {
            return result.Errors.ToValidationException();
        }
        
        logger.LogInformation("User account confirmed for {email}.", user.Email);
        
        return Result.Success();
    }

    public async Task<Result<IUser>> CheckPasswordSignInAsync(string email, string password, CancellationToken cancellationToken = default)
    {
        var user = await userManager.FindByEmailAsync(email);
        if (user is null) return "This operation is invalid.".ToInvalidOperationException();

        var result = await signInManager.CheckPasswordSignInAsync(user, password, false);

        if (result.IsLockedOut) return "User account is locked out.".ToInvalidOperationException();
        if (result.IsNotAllowed) return "User account is not allowed.".ToInvalidOperationException();
        if (result.RequiresTwoFactor) return "User account requires two factor authentication.".ToInvalidOperationException();
        
        return user;
    }

    public async Task<Result<IUser>> GetUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var usarIdentity = await userManager.FindByIdAsync(userId.ToString());
        return usarIdentity ?? Result<IUser>.Fail("This operation is invalid.".ToInvalidOperationException());
    }
}