using System.Net.Http.Json;
using System.Web;
using HelpDesk.Api.Dtos.Requests;
using HelpDesk.Api.Dtos.Responses;
using HelpDesk.Api.Models.Enums;

namespace HelpDesk.Ui.Services;

public class TicketService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public TicketService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    private HttpClient Client => _httpClientFactory.CreateClient("api");

    public async Task<List<TicketDto>> GetAllAsync(TicketQuery? query = null)
    {
        var url = BuildUrl("/api/tickets", query);
        return await Client.GetFromJsonAsync<List<TicketDto>>(url) ?? new();
    }

    public async Task<List<TicketDto>> GetMyTicketsAsync(TicketQuery? query = null)
    {
        var url = BuildUrl("/api/tickets/mine", query);
        return await Client.GetFromJsonAsync<List<TicketDto>>(url) ?? new();
    }

    public async Task<TicketDto?> GetByIdAsync(int id)
    {
        return await Client.GetFromJsonAsync<TicketDto>($"/api/tickets/{id}");
    }

    public async Task<TicketDto> CreateAsync(CreateTicketRequest request)
    {
        var resp = await Client.PostAsJsonAsync("/api/tickets", request);
        resp.EnsureSuccessStatusCode();
        return (await resp.Content.ReadFromJsonAsync<TicketDto>())!;
    }

    public async Task<TicketDto> UpdateAsync(int id, UpdateTicketRequest request)
    {
        var resp = await Client.PutAsJsonAsync($"/api/tickets/{id}", request);
        resp.EnsureSuccessStatusCode();
        return (await resp.Content.ReadFromJsonAsync<TicketDto>())!;
    }

    public async Task UpdateStatusAsync(int id, Status status)
    {
        var resp = await Client.PatchAsJsonAsync($"/api/tickets/{id}/status",
            new UpdateTicketStatusRequest(status));
        resp.EnsureSuccessStatusCode();
    }

    private static string BuildUrl(string path, TicketQuery? query)
    {
        if (query is null) return path;

        var qs = HttpUtility.ParseQueryString(string.Empty);

        if (query.Status.HasValue)
            qs["Status"] = query.Status.Value.ToString();

        if (!string.IsNullOrEmpty(query.Tag))
            qs["Tag"] = query.Tag;

        if (!string.IsNullOrEmpty(query.Author))
            qs["Author"] = query.Author;

        if (!string.IsNullOrEmpty(query.Q))
            qs["Q"] = query.Q;

        var str = qs.ToString();
        return string.IsNullOrEmpty(str) ? path : $"{path}?{str}";
    }
}
