using Microsoft.AspNetCore.Components.Forms;
using HelpDesk.Api.Dtos.Responses;

namespace HelpDesk.Ui.Services;

public class AttachmentService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public AttachmentService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    private HttpClient Client => _httpClientFactory.CreateClient("api");

    public string GetImageUrl(Guid attachmentId)
    {
        return $"{Client.BaseAddress}api/attachments/{attachmentId}";
    }

    public async Task<AttachmentResponse> UploadAsync(int ticketId, IBrowserFile file)
    {
        using var content = new MultipartFormDataContent();
        using var stream = file.OpenReadStream(maxAllowedSize: 10 * 1024 * 1024);
        using var fileContent = new StreamContent(stream);
        fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpeg");
        content.Add(fileContent, "file", file.Name);

        var resp = await Client.PostAsync($"/api/tickets/{ticketId}/attachments", content);
        resp.EnsureSuccessStatusCode();
        return (await resp.Content.ReadFromJsonAsync<AttachmentResponse>())!;
    }

    public async Task DeleteAsync(int ticketId, Guid attachmentId)
    {
        var resp = await Client.DeleteAsync($"/api/tickets/{ticketId}/attachments/{attachmentId}");
        resp.EnsureSuccessStatusCode();
    }
}
