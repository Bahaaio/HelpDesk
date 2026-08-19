namespace HelpDesk.Ui.Services;

public class AuthState
{
    private readonly AuthService _authService;

    public AuthState(AuthService authService)
    {
        _authService = authService;
    }

    public bool IsAuthenticated { get; private set; }

    public string Username { get; private set; } = "";

    public string Role { get; private set; } = "";

    public async Task<bool> CheckAsync()
    {
        var user = await _authService.GetCurrentUserAsync();
        if (user is not null)
        {
            SetAuthenticated(user.UserName, user.Role);
            return true;
        }

        SetUnauthenticated();
        return false;
    }

    public void SetAuthenticated(string username, string role)
    {
        IsAuthenticated = true;
        Username = username;
        Role = role;
    }

    public void SetUnauthenticated()
    {
        IsAuthenticated = false;
        Username = "";
        Role = "";
    }
}