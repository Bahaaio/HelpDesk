namespace Workbench.ClientServices;

/// <summary>
///     Blazor-side snapshot of the current user's authentication state.
///     Populated by calling <see cref="Check" /> once per circuit/page load;
///     components read the properties instead of touching HTTP context.
/// </summary>
public interface IAuthState
{
    /// <summary>
    ///     Whether the current user is authenticated.
    /// </summary>
    bool IsAuthenticated { get; }

    /// <summary>
    ///     The current user's username, or an empty string when unauthenticated.
    /// </summary>
    string Username { get; }

    /// <summary>
    ///     The current user's role (e.g. <see cref="Workbench.Modules.Auth.Enums.Role" />),
    ///     or an empty string when unauthenticated.
    /// </summary>
    string Role { get; }

    /// <summary>
    ///     Refreshes the state from the current <c>ICurrentUser</c> principal.
    ///     Safe to call when no authentication context exists.
    /// </summary>
    void Check();
}
