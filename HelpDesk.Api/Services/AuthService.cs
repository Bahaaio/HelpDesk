using HelpDesk.Api.Dtos.Requests;
using HelpDesk.Api.Exceptions;
using HelpDesk.Api.Models;
using HelpDesk.Api.Models.Enums;
using Microsoft.AspNetCore.Identity;

namespace HelpDesk.Api.Services;

public class AuthService(
    UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager)
{
    public async Task Register(RegisterRequest request)
    {
        var user = new ApplicationUser
        {
            UserName = request.Username,
            Email = request.Email
        };

        var result = await userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
            throw new BadRequestException(string.Join(", ",
                result.Errors.Select(e => e.Description))
            );

        await userManager.AddToRoleAsync(user, Role.Employee);
        await signInManager.SignInAsync(user, request.RememberMe);
    }

    public async Task Login(LoginRequest request)
    {
        var user = await userManager.FindByNameAsync(request.Username);

        if (user is null)
            throw new UnauthorizedException("Invalid username or password");

        var result = await signInManager.PasswordSignInAsync(
            user,
            request.Password,
            request.RememberMe,
            false
        );

        if (!result.Succeeded)
            throw new UnauthorizedException("Invalid username or password");
    }

    public async Task Logout()
    {
        await signInManager.SignOutAsync();
    }
}