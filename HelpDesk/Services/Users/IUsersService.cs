using HelpDesk.Dtos.Responses;

namespace HelpDesk.Services.Users;

/// <summary>
///     Provides access to the current authenticated user's profile information.
/// </summary>
public interface IUsersService
{
    /// <summary>
    ///     Returns the current user's username and role.
    /// </summary>
    UserDto GetCurrentUser();
}
