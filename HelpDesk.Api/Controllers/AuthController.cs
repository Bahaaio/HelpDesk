using HelpDesk.Api.Dtos.Requests;
using HelpDesk.Api.Dtos.Responses;
using HelpDesk.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HelpDesk.Api.Controllers;

[AllowAnonymous]
[ApiController]
[Route("api/[controller]")]
public class AuthController(AuthService authService) : ControllerBase
{
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

    [HttpPost("logout")]
    public async Task<ActionResult> Logout()
    {
        var refreshToken = Request.Cookies[RefreshTokenCookieName];
        if (string.IsNullOrEmpty(refreshToken))
            return Unauthorized();

        await authService.Logout(refreshToken);
        RemoveCookie();

        return NoContent();
    }

    private void SetCookie(string refreshToken)
    {
        var cookie = new CookieBuilder
        {
            HttpOnly = true,
            SameSite = SameSiteMode.Strict
        }.Build(HttpContext);

        Response.Cookies.Append(RefreshTokenCookieName, refreshToken, cookie);
    }

    private void RemoveCookie()
    {
        Response.Cookies.Delete(RefreshTokenCookieName);
    }
}