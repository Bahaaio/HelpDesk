using HelpDesk.Api.Dtos.Requests;
using HelpDesk.Api.Exceptions;
using HelpDesk.Api.Models;
using HelpDesk.Api.Models.Enums;
using Microsoft.AspNetCore.Identity;

namespace HelpDesk.Api.Services;

public class AuthService : IAuthService
{
    private readonly ILogger<AuthService> _logger;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public AuthService(UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager, ILogger<AuthService> logger)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _logger = logger;
    }

    public async Task Register(RegisterRequest request)
    {
        var user = new ApplicationUser
        {
            UserName = request.Username,
            Email = request.Email
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
            throw new BadRequestException(string.Join(", ",
                result.Errors.Select(e => e.Description)));

        _logger.LogInformation("User created: {username}", user.UserName);

        await _userManager.AddToRoleAsync(user, Role.Employee);
        await _signInManager.SignInAsync(user, request.RememberMe);
    }

    public async Task Login(LoginRequest request)
    {
        var user = await _userManager.FindByNameAsync(request.Username);

        if (user is null)
        {
            _logger.LogWarning("User not found: {username}", request.Username);
            throw new UnauthorizedException("Invalid username or password");
        }

        var result = await _signInManager.PasswordSignInAsync(
            user,
            request.Password,
            request.RememberMe,
            false
        );

        if (!result.Succeeded)
        {
            _logger.LogWarning("Invalid password for user: {username}", request.Username);
            throw new UnauthorizedException("Invalid username or password");
        }
    }

    public async Task Logout()
    {
        await _signInManager.SignOutAsync();
    }
}