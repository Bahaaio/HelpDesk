using System.Net.Http.Json;
using HelpDesk.Dtos.Requests;
using HelpDesk.Dtos.Responses;
using HelpDesk.Models.Enums;

namespace HelpDesk.ClientServices;

public class VoteService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public VoteService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    private HttpClient Client => _httpClientFactory.CreateClient("api");

    public async Task<VoteValue?> GetMyVoteAsync(int ticketId)
    {
        var dto = await Client.GetFromJsonAsync<VoteDto>(
            $"/api/tickets/{ticketId}/votes/mine");
        return dto?.Vote;
    }

    public async Task VoteAsync(int ticketId, VoteValue value)
    {
        var resp = await Client.PostAsJsonAsync($"/api/tickets/{ticketId}/votes",
            new VoteRequest(value));
        resp.EnsureSuccessStatusCode();
    }

    public async Task DeleteVoteAsync(int ticketId)
    {
        var resp = await Client.DeleteAsync($"/api/tickets/{ticketId}/votes/mine");
        resp.EnsureSuccessStatusCode();
    }
}
