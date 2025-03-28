﻿using AdventEcho.Identity.Application.Shared.Register;
using AdventEcho.Identity.Domain.Users.Services;
using AdventEcho.Kernel.Extensions;

namespace AdventEcho.Identity.Application.Register;

internal sealed class RegisterUserHandler(IUserService userService) : IRegisterUserHandler
{
    public async Task<Result> Handle(RegisterUserRequest request, CancellationToken cancellationToken = default)
    {
        if(request.Name is null) return "Name is required".ToArgumentNullException();
        if(request.Email is null) return "Email is required".ToArgumentNullException();
        if(request.Password is null) return "Password is required".ToArgumentNullException();
        
        var result = await userService.RegisterAsync(
            request.Name, 
            request.Email, 
            request.Password, 
            cancellationToken
        ); 
        
        return result;
    }
}