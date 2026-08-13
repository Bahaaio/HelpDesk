using System.Security.Claims;
using HelpDesk.Api.Dtos.Responses;

namespace HelpDesk.Api.Services;

public class UsersService
{
    public UserDto GetCurrentUser(ClaimsPrincipal user)
    {
        var role = user.FindFirstValue(ClaimTypes.Role)!;
        var userName = user.FindFirstValue(ClaimTypes.Name)!;

        return new UserDto(userName, role);
    }
}