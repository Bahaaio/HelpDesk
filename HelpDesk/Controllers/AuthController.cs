using HelpDesk.Dtos.Requests;
using HelpDesk.Exceptions;
using HelpDesk.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HelpDesk.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : Controller
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromForm] LoginRequest loginRequest,
        string returnUrl = "/issues")
    {
        try
        {
            await _authService.Login(loginRequest);
            return RedirectToLocal(returnUrl, "/issues");
        }
        catch (Exception)
        {
            return Redirect("/login?error=Invalid+username+or+password");
        }
    }

    [AllowAnonymous]
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

    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> Logout(string returnUrl = "/login")
    {
        await _authService.Logout();
        return RedirectToLocal(returnUrl, "/login");
    }

    private IActionResult RedirectToLocal(string returnUrl, string fallback) =>
        Url.IsLocalUrl(returnUrl) ? LocalRedirect(returnUrl) : LocalRedirect(fallback);
}