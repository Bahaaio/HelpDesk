using System.Security.Claims;

namespace HelpDesk.Services.Auth;

public class CurrentUser : ICurrentUser
{
    public CurrentUser(IHttpContextAccessor httpContextAccessor)
    {
        Principal = httpContextAccessor.HttpContext!.User;
    }

    public int Id => int.Parse(Principal.FindFirstValue(ClaimTypes.NameIdentifier)!);
    public string UserName => Principal.FindFirstValue(ClaimTypes.Name)!;
    public string Role => Principal.FindFirstValue(ClaimTypes.Role)!;
    public ClaimsPrincipal Principal { get; }
}
