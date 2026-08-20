using HelpDesk.Dtos.Requests;
using HelpDesk.Exceptions;
using HelpDesk.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HelpDesk.Controllers;

[AllowAnonymous]
[ApiController]
[Route("api/[controller]")]
public class AuthController : Controller
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromForm] LoginRequest loginRequest,
        string returnUrl = "/tickets")
    {
        try
        {
            await _authService.Login(loginRequest);
            return Redirect(returnUrl);
        }
        catch (Exception)
        {
            return Redirect("/login?error=Invalid+username+or+password");
        }
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromForm] RegisterRequest registerRequest)
    {
        try
        {
            await _authService.Register(registerRequest);
            return Redirect("/");
        }
        catch (BadRequestException ex)
        {
            return Redirect($"/register?error={Uri.EscapeDataString(ex.Message)}");
        }
        catch (Exception)
        {
            return Redirect("/register?error=Registration+failed");
        }
    }

    [HttpGet("logout")]
    public async Task<IActionResult> Logout()
    {
        await _authService.Logout();
        return Redirect("/login");
    }
}