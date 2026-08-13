using System.Net.Http.Json;
using HelpDesk.Api.Dtos.Responses;

namespace HelpDesk.Ui.Services;

public class AuthState
{
    private readonly IHttpClientFactory _httpClientFactory;
    private bool _isAuthenticated;
    private string _username = "";
    private string _role = "";

    public AuthState(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public bool IsAuthenticated => _isAuthenticated;
    public string Username => _username;
    public string Role => _role;

    public async Task<bool> CheckAsync()
    {
        try
        {
            var client = _httpClientFactory.CreateClient("api");
            var resp = await client.GetAsync("/api/users/me");

            if (resp.IsSuccessStatusCode)
            {
                var user = await resp.Content.ReadFromJsonAsync<UserDto>();
                if (user is not null)
                {
                    _isAuthenticated = true;
                    _username = user.UserName;
                    _role = user.Role;
                    return true;
                }
            }

            SetUnauthenticated();
            return false;
        }
        catch
        {
            SetUnauthenticated();
            return false;
        }
    }

    public void SetAuthenticated(string username, string role)
    {
        _isAuthenticated = true;
        _username = username;
        _role = role;
    }

    public void SetUnauthenticated()
    {
        _isAuthenticated = false;
        _username = "";
        _role = "";
    }
}
