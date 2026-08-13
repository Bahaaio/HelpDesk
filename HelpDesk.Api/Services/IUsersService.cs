using HelpDesk.Api.Dtos.Responses;

namespace HelpDesk.Api.Services;

public interface IUsersService
{
    UserDto GetCurrentUser();
}