using Workbench.Modules.Auth.Services;
using Workbench.Modules.Users.Dtos;

namespace Workbench.Modules.Users.Services.Implementations;

public class UsersService : IUsersService
{
    private readonly ICurrentUser _user;

    public UsersService(ICurrentUser user)
    {
        _user = user;
    }

    public UserDto GetCurrentUser() => new(_user.UserName);
}