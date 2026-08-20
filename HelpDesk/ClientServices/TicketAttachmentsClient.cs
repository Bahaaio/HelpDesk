using System.Net.Http.Headers;
using HelpDesk.Dtos.Responses;
using Microsoft.AspNetCore.Components.Forms;

namespace HelpDesk.ClientServices;

public class TicketAttachmentsClient : ITicketAttachmentsClient
{
    private readonly HttpClient _httpClient;

    public TicketAttachmentsClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<AttachmentDto>> GetAll(int ticketId) =>
        await _httpClient.GetFromJsonAsync<List<AttachmentDto>>(
            $"/api/tickets/{ticketId}/attachments") ?? new List<AttachmentDto>();

    public async Task<AttachmentDto> Add(int ticketId, IBrowserFile file)
    {
        using var content = new MultipartFormDataContent();
        await using var stream = file.OpenReadStream(10 * 1024 * 1024);
        using var streamContent = new StreamContent(stream);
        streamContent.Headers.ContentType = new MediaTypeHeaderValue(
            file.ContentType ?? "application/octet-stream");
        content.Add(streamContent, "file", file.Name);

        var response = await _httpClient.PostAsync($"/api/tickets/{ticketId}/attachments", content);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<AttachmentDto>()
               ?? throw new InvalidOperationException("Failed to deserialize attachment response");
    }

    public async Task Delete(int ticketId, Guid attachmentId)
    {
        var response =
            await _httpClient.DeleteAsync($"/api/tickets/{ticketId}/attachments/{attachmentId}");
        response.EnsureSuccessStatusCode();
    }
}