using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace HelpDesk.Services.Auth;

/// <summary>
///     Custom AuthenticationStateProvider that reads from IHttpContextAccessor.
///     Works in both Blazor interactive context and API controller context.
/// </summary>
public class HttpContextAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HttpContextAuthenticationStateProvider(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var user = _httpContextAccessor.HttpContext?.User ?? new ClaimsPrincipal();
        return Task.FromResult(new AuthenticationState(user));
    }
}
