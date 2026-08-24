using HelpDesk.Modules.Auth.Services;

namespace HelpDesk.ClientServices;

public class AuthState
{
    private readonly ICurrentUser _currentUser;

    public AuthState(ICurrentUser currentUser)
    {
        _currentUser = currentUser;
    }

    public bool IsAuthenticated { get; private set; }
    public string Username { get; private set; } = "";
    public string Role { get; private set; } = "";

    public void Check()
    {
        try
        {
            if (_currentUser.Principal.Identity?.IsAuthenticated == true)
            {
                IsAuthenticated = true;
                Username = _currentUser.UserName;
                Role = _currentUser.Role;
                return;
            }
        }
        catch
        {
            // Not authenticated or not in a circuit
        }

        IsAuthenticated = false;
        Username = "";
        Role = "";
    }
}