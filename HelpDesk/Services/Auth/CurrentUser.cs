using System.Security.Claims;

namespace HelpDesk.Services.Auth;

public class CurrentUser : ICurrentUser
{
    private readonly ClaimsPrincipal _principal;

    public CurrentUser(IHttpContextAccessor contextAccessor)
    {
        _principal = contextAccessor.HttpContext?.User ?? new ClaimsPrincipal();
    }

    public int Id => int.Parse(Principal.FindFirstValue(ClaimTypes.NameIdentifier)!);
    public string UserName => Principal.FindFirstValue(ClaimTypes.Name)!;
    public string Role => Principal.FindFirstValue(ClaimTypes.Role)!;
    public ClaimsPrincipal Principal => _principal;
}
