using System.Net.Http.Json;

namespace HelpDesk.Ui.Services;

public class InviteService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public InviteService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    private HttpClient Client => _httpClientFactory.CreateClient("api");

    public async Task<InviteDto> InviteAsync(int validDays = 7)
    {
        var resp = await Client.PostAsJsonAsync("/api/invite", new { ValidDays = validDays });
        resp.EnsureSuccessStatusCode();
        return (await resp.Content.ReadFromJsonAsync<InviteDto>())!;
    }
}

public record InviteDto(string Code, DateTime ExpiresAt);
