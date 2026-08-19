using System.Net.Http.Json;
using HelpDesk.Dtos.Requests;
using HelpDesk.Dtos.Responses;

namespace HelpDesk.Ui.Services;

public class CommentService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public CommentService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    private HttpClient Client => _httpClientFactory.CreateClient("api");

    public async Task<List<CommentDto>> GetAllAsync(int ticketId)
    {
        return await Client.GetFromJsonAsync<List<CommentDto>>(
            $"/api/tickets/{ticketId}/comments") ?? new();
    }

    public async Task<CommentDto> CreateAsync(int ticketId, CreateCommentRequest request)
    {
        var resp = await Client.PostAsJsonAsync($"/api/tickets/{ticketId}/comments", request);
        resp.EnsureSuccessStatusCode();
        return (await resp.Content.ReadFromJsonAsync<CommentDto>())!;
    }

    public async Task DeleteAsync(int ticketId, int commentId)
    {
        var resp = await Client.DeleteAsync($"/api/tickets/{ticketId}/comments/{commentId}");
        resp.EnsureSuccessStatusCode();
    }
}
