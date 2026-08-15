using System.Net.Http.Json;
using HelpDesk.Api.Dtos.Requests;
using HelpDesk.Api.Dtos.Responses;

namespace HelpDesk.Ui.Services;

public class TagService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public TagService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    private HttpClient Client => _httpClientFactory.CreateClient("api");

    public async Task<List<TagDto>> GetAllAsync()
    {
        return await Client.GetFromJsonAsync<List<TagDto>>("/api/tags") ?? new();
    }

    public async Task<TagDto> CreateAsync(CreateTagRequest request)
    {
        var resp = await Client.PostAsJsonAsync("/api/tags", request);
        resp.EnsureSuccessStatusCode();
        return (await resp.Content.ReadFromJsonAsync<TagDto>())!;
    }

    public async Task<TagDto> UpdateAsync(string name, UpdateTagRequest request)
    {
        var resp = await Client.PutAsJsonAsync($"/api/tags/{name}", request);
        resp.EnsureSuccessStatusCode();
        return (await resp.Content.ReadFromJsonAsync<TagDto>())!;
    }

    public async Task<List<string>> UpdateTicketTagsAsync(int ticketId, List<string> tags)
    {
        var resp = await Client.PutAsJsonAsync($"/api/tickets/{ticketId}/tags", tags);
        resp.EnsureSuccessStatusCode();
        return (await resp.Content.ReadFromJsonAsync<List<string>>()) ?? new();
    }

    public async Task DeleteAsync(string name)
    {
        var resp = await Client.DeleteAsync($"/api/tags/{name}");
        resp.EnsureSuccessStatusCode();
    }
}
