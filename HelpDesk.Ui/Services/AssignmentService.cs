using System.Net.Http.Json;
using System.Web;
using HelpDesk.Api.Dtos.Requests;
using HelpDesk.Api.Dtos.Responses;

namespace HelpDesk.Ui.Services;

public class AssignmentService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public AssignmentService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    private HttpClient Client => _httpClientFactory.CreateClient("api");

    public async Task AssignAsync(int ticketId)
    {
        var resp = await Client.PostAsync($"/api/tickets/{ticketId}/assignments", null);
        resp.EnsureSuccessStatusCode();
    }

    public async Task UnassignAsync(int ticketId)
    {
        var resp = await Client.DeleteAsync($"/api/tickets/{ticketId}/assignments");
        resp.EnsureSuccessStatusCode();
    }

    public async Task<List<TicketDto>> GetAssignedTicketsAsync(TicketQuery? query = null)
    {
        var url = BuildUrl("/api/tickets/mine/assigned", query);
        return await Client.GetFromJsonAsync<List<TicketDto>>(url) ?? new();
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

        if (!string.IsNullOrEmpty(query.Query))
            qs["Query"] = query.Query;

        var str = qs.ToString();
        return string.IsNullOrEmpty(str) ? path : $"{path}?{str}";
    }
}
