using HelpDesk.Modules.Users.Dtos;

namespace HelpDesk.Modules.Users.Services;

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