using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace HelpDesk.Services.Auth;

public class CurrentUser : ICurrentUser
{
    private ClaimsPrincipal _principal;

    public CurrentUser(AuthenticationStateProvider authStateProvider)
    {
        var task = authStateProvider.GetAuthenticationStateAsync();
        task.Wait();
        _principal = task.Result.User;
    }

    public int Id => int.Parse(Principal.FindFirstValue(ClaimTypes.NameIdentifier)!);
    public string UserName => Principal.FindFirstValue(ClaimTypes.Name)!;
    public string Role => Principal.FindFirstValue(ClaimTypes.Role)!;
    public ClaimsPrincipal Principal => _principal;
}
