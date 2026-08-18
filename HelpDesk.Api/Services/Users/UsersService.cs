using HelpDesk.Api.Dtos.Responses;
using HelpDesk.Api.Services.Auth;

namespace HelpDesk.Api.Services.Users;

public class UsersService : IUsersService
{
    private readonly ICurrentUser _user;

    public UsersService(ICurrentUser user)
    {
        _user = user;
    }

    public UserDto GetCurrentUser() => new(_user.UserName, _user.Role);
}