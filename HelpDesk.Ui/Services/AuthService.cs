using System.Net.Http.Json;
using HelpDesk.Api.Dtos.Responses;

namespace HelpDesk.Ui.Services;

public class AuthService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly AuthState _authState;

    public AuthService(IHttpClientFactory httpClientFactory, AuthState authState)
    {
        _httpClientFactory = httpClientFactory;
        _authState = authState;
    }

    private HttpClient Client => _httpClientFactory.CreateClient("api");

    public async Task<HttpResponseMessage> LoginAsync(string username, string password, bool rememberMe)
    {
        var resp = await Client.PostAsJsonAsync("/api/auth/login",
            new { Username = username, Password = password, RememberMe = rememberMe });
        return resp;
    }

    public async Task FetchCurrentUserAsync()
    {
        var me = await Client.GetFromJsonAsync<UserDto>("/api/users/me");
        if (me is not null)
        {
            _authState.SetAuthenticated(me.UserName, me.Role);
        }
    }

    public async Task RegisterAsync(string username, string email, string password, bool rememberMe)
    {
        var resp = await Client.PostAsJsonAsync("/api/auth/register",
            new { Username = username, Email = email, Password = password, RememberMe = rememberMe });
        resp.EnsureSuccessStatusCode();
    }

    public async Task LogoutAsync()
    {
        await Client.PostAsync("/api/auth/logout", null);
        _authState.SetUnauthenticated();
    }
}
