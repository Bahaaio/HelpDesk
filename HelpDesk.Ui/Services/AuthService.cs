using HelpDesk.Dtos.Responses;

namespace HelpDesk.Ui.Services;

public class AuthService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public AuthService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    private HttpClient Client => _httpClientFactory.CreateClient("api");

    public async Task<HttpResponseMessage> LoginAsync(string username, string password,
        bool rememberMe) =>
        await Client.PostAsJsonAsync("/api/auth/login",
            new { Username = username, Password = password, RememberMe = rememberMe });

    public async Task<HttpResponseMessage> RegisterAsync(string username, string email,
        string password, string? code) =>
        await Client.PostAsJsonAsync("/api/auth/register",
            new
            {
                Username = username, Email = email, Password = password, RememberMe = false,
                Code = code
            });

    public async Task LogoutAsync()
    {
        await Client.PostAsync("/api/auth/logout", null);
    }

    public async Task<UserDto?> GetCurrentUserAsync()
    {
        try
        {
            return await Client.GetFromJsonAsync<UserDto>("/api/users/me");
        }
        catch
        {
            return null;
        }
    }
}
