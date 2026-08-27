using Workbench.Modules.Auth.Dtos;

namespace Workbench.Modules.Auth.Services;

/// <summary>
///     Handles user authentication, registration, and session management.
/// </summary>
public interface IAuthService
{
    /// <summary>
    ///     Registers a new employee user account.
    /// </summary>
    /// <param name="request">The registration details including username, email, and password.</param>
    Task Register(RegisterRequest request);

    /// <summary>
    ///     Authenticates a user and creates a session.
    /// </summary>
    /// <param name="request">The login credentials including username and password.</param>
    Task Login(LoginRequest request);

    /// <summary>
    ///     Ends the current user session.
    /// </summary>
    Task Logout();
}
