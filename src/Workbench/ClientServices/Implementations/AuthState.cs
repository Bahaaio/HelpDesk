using Workbench.Modules.Auth.Services;

namespace Workbench.ClientServices.Implementations;

public class AuthState : IAuthState
{
    private readonly ICurrentUser _currentUser;

    public AuthState(ICurrentUser currentUser)
    {
        _currentUser = currentUser;
    }

    public bool IsAuthenticated { get; private set; }
    public string Username { get; private set; } = "";

    public void Check()
    {
        try
        {
            if (_currentUser.Principal.Identity?.IsAuthenticated == true)
            {
                IsAuthenticated = true;
                Username = _currentUser.UserName;
                return;
            }
        }
        catch
        {
            // Not authenticated or not in a circuit
        }

        IsAuthenticated = false;
        Username = "";
    }
}