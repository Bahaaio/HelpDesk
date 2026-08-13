using System.Net.Http.Json;
using HelpDesk.Api.Dtos.Requests;
using HelpDesk.Api.Dtos.Responses;
using HelpDesk.Api.Models.Enums;

namespace HelpDesk.Ui.Services;

public class VoteService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public VoteService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    private HttpClient Client => _httpClientFactory.CreateClient("api");

    public async Task<VoteResponse> GetMyVoteAsync(int ticketId)
    {
        return await Client.GetFromJsonAsync<VoteResponse>(
            $"/api/tickets/{ticketId}/votes/mine") ?? new VoteResponse(VoteValue.None);
    }

    public async Task VoteAsync(int ticketId, VoteValue value)
    {
        var resp = await Client.PostAsJsonAsync($"/api/tickets/{ticketId}/votes",
            new VoteRequest(value));
        resp.EnsureSuccessStatusCode();
    }
}
