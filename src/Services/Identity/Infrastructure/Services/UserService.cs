using System.Web;
using AdventEcho.Identity.Application;
using AdventEcho.Identity.Application.Services.Users;
using AdventEcho.Identity.Domain.Users;
using AdventEcho.Identity.Infrastructure.Extensions;
using AdventEcho.Kernel.Application.Shared.Messages.Results;
using AdventEcho.Kernel.Infrastructure.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AdventEcho.Identity.Infrastructure.Services;

internal class UserService(
    UserManager<AdventEchoUser> userManager,
    SignInManager<AdventEchoUser> signInManager,
    IUserStore<AdventEchoUser> userStore,
    ILogger<UserService> logger,
    IOptions<AdventEchoIdentityOptions> options,
    IEmailSender<AdventEchoUser> emailSender,
    IUserRepository userRepository) : IUserService
{
    private readonly AdventEchoIdentityOptions _options = options.Value;
    
    
    public async Task<Result> RegisterAsync(NameVo name, string email, string password, CancellationToken cancellationToken = default)
    {
        var user = new AdventEchoUser(name);
        
        await userStore.SetUserNameAsync(user, email, cancellationToken);
        await ((IUserEmailStore<AdventEchoUser>)userStore).SetEmailAsync(user, email, cancellationToken);
        
        var result = await userManager.CreateAsync(user, password);
        if (!result.Succeeded) return EchoResults.Validation(result.Errors.ToDictionary());
        
        logger.LogInformation("User account created for {email} and password set.", email);
        
        var userId = await userManager.GetUserIdAsync(user);
        var code = await userManager.GenerateEmailConfirmationTokenAsync(user);
        
        code = HttpUtility.UrlEncode(code);
        
        var callbackUrl = $"{_options.Domains.Web}/account/confirm-email?userId={userId}&code={code}";
        
        await emailSender.SendConfirmationLinkAsync(user, email, callbackUrl);
        
        return Result.Success();
    }

    public async Task<Result> ConfirmEmailAsync(Guid userId, string token, CancellationToken cancellationToken = default)
    {
        var user = await userManager.FindByIdAsync(userId.ToString());
        if (user is null) return EchoResults.BadRequest("This operation is invalid.");
        
        var result = await userManager.ConfirmEmailAsync(user, token);
        
        if (!result.Succeeded) return EchoResults.Validation(result.Errors.ToDictionary());
        
        
        logger.LogInformation("User account confirmed for {email}.", user.Email);
        
        return Result.Success();
    }

    public async Task<Result<User>> CheckPasswordSignInAsync(string email, string password, CancellationToken cancellationToken = default)
    {
        var user = await userManager.FindByEmailAsync(email);
        if (user is null) return EchoResults<User>.BadRequest("This operation is invalid.");

        var result = await signInManager.CheckPasswordSignInAsync(user, password, false);

        if (result.IsLockedOut) return EchoResults<User>.Forbidden("User account is locked out.");
        if (result.IsNotAllowed) return EchoResults<User>.Forbidden("User account is not allowed.");
        if (result.RequiresTwoFactor) return EchoResults<User>.Forbidden("User account requires two factor authentication.");
        
        var domainUser = await userRepository.GetByIdAsync(user.Id);
        return domainUser ?? EchoResults<User>.BadRequest("This operation is invalid.");
    }
}