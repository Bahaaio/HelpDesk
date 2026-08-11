using HelpDesk.Api.Dtos.Requests;
using HelpDesk.Api.Dtos.Responses;
using HelpDesk.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace HelpDesk.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(AuthService authService) : ControllerBase
{
    // TODO: move to sepearate service
    private const string RefreshTokenCookieName = "refreshToken";

    [HttpPost("register")]
    public async Task<ActionResult<AuthResponse>> Register(RegisterRequest registerRequest)
    {
        var result = await authService.Register(registerRequest);
        SetCookie(result.RefreshToken);

        return Ok(new AuthResponse(result.AccessToken));
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponse>> Login(LoginRequest loginRequest)
    {
        var result = await authService.Login(loginRequest);
        SetCookie(result.RefreshToken);

        return Ok(new AuthResponse(result.AccessToken));
    }

    [HttpPost("refresh")]
    public async Task<ActionResult<AuthResponse>> Refresh()
    {
        var refreshToken = Request.Cookies[RefreshTokenCookieName];
        if (string.IsNullOrEmpty(refreshToken))
            return Unauthorized();

        var result = await authService.Refresh(refreshToken);
        SetCookie(result.RefreshToken);

        return Ok(new AuthResponse(result.AccessToken));
    }

    [HttpPost]
    private void SetCookie(string refreshToken)
    {
        var cookie = new CookieBuilder
        {
            HttpOnly = true,
            SameSite = SameSiteMode.Strict
        }.Build(HttpContext);

        Response.Cookies.Append(RefreshTokenCookieName, refreshToken, cookie);
    }
}