using HelpDesk.Modules.Auth.Services;
using HelpDesk.Modules.Users.Dtos;

namespace HelpDesk.Modules.Users.Services;

public class UsersService : IUsersService
{
    private readonly ICurrentUser _user;

    public UsersService(ICurrentUser user)
    {
        _user = user;
    }

    public UserDto GetCurrentUser() => new(_user.UserName, _user.Role);
}