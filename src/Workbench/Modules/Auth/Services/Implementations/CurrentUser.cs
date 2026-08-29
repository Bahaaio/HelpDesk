using System.Security.Claims;

namespace Workbench.Modules.Auth.Services.Implementations;

public class CurrentUser : ICurrentUser
{
    public CurrentUser(IHttpContextAccessor contextAccessor)
    {
        Principal = contextAccessor.HttpContext?.User ?? new ClaimsPrincipal();
    }

    public int Id => int.Parse(Principal.FindFirstValue(ClaimTypes.NameIdentifier)!);
    public string UserName => Principal.FindFirstValue(ClaimTypes.Name)!;
    public ClaimsPrincipal Principal { get; }
}
