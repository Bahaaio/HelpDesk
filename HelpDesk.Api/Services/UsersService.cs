using HelpDesk.Api.Dtos.Responses;

namespace HelpDesk.Api.Services;

public class UsersService
{
    private readonly ICurrentUser _user;

    public UsersService(ICurrentUser user)
    {
        _user = user;
    }

    public UserDto GetCurrentUser()
    {
        return new UserDto(_user.UserName, _user.Role);
    }
}