using HelpDesk.Api.Dtos.Requests;
using HelpDesk.Api.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HelpDesk.Api.Controllers;

[AllowAnonymous]
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<ActionResult> Register(RegisterRequest registerRequest)
    {
        await _authService.Register(registerRequest);
        return Created();
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login(LoginRequest loginRequest)
    {
        await _authService.Login(loginRequest);
        return Ok();
    }

    [HttpPost("logout")]
    public async Task<ActionResult> Logout()
    {
        await _authService.Logout();
        return NoContent();
    }
}