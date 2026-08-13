using System.Security.Claims;

namespace HelpDesk.Api.Services;

public interface ICurrentUser
{
    public int Id { get; }
    public string UserName { get; }
    public string Role { get; }
    public ClaimsPrincipal Principal { get; }
}