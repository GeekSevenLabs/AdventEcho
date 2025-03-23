using AdventEcho.Identity.Application.Shared;
using AdventEcho.Identity.Application.Shared.Login;
using AdventEcho.Identity.Application.Shared.Register;
using AdventEcho.Kernel.Extensions;

namespace AdventEcho.Identity.Application;

public class AccountService(
    IUserService userService,
    ITokenService tokenService) : IAccountService
{
    public async Task<Result> RegisterAsync(RegisterUserRequest request)
    {
        return await userService.RegisterAsync(request.Name.Required(), request.Email.Required(), request.Password.Required());
    }

    public async Task<Result<LoginResponse>> LoginAsync(LoginRequest request)
    {
        var user = await userService.FindByEmailAsync(request.Email.Required());
        if (user is null) return Result.Fail<LoginResponse>("Invalid email or password.");
        
        
        
        var accessToken = await tokenService.GenerateToken(user);
        var refreshToken = await tokenService.GenerateRefreshToken(user, accessToken);
        
        return Result.Ok(new LoginResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            UserId = user.Id
        });
    }
}