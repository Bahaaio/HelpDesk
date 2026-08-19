using Microsoft.AspNetCore.Components.Forms;
using HelpDesk.Dtos.Responses;

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

    public async Task<AttachmentDto> UploadAsync(int ticketId, IBrowserFile file)
    {
        using var content = new MultipartFormDataContent();
        using var stream = file.OpenReadStream(maxAllowedSize: 10 * 1024 * 1024);
        using var fileContent = new StreamContent(stream);
        fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(
            file.ContentType ?? "application/octet-stream");
        content.Add(fileContent, "file", file.Name);

        var resp = await Client.PostAsync($"/api/tickets/{ticketId}/attachments", content);
        resp.EnsureSuccessStatusCode();
        return (await resp.Content.ReadFromJsonAsync<AttachmentDto>())!;
    }

    public async Task DeleteAsync(int ticketId, Guid attachmentId)
    {
        var resp = await Client.DeleteAsync($"/api/tickets/{ticketId}/attachments/{attachmentId}");
        resp.EnsureSuccessStatusCode();
    }

    public async Task<string?> GetFileNameAsync(Guid attachmentId)
    {
        try
        {
            using var resp = await Client.GetAsync($"/api/attachments/{attachmentId}", HttpCompletionOption.ResponseHeadersRead);
            var cd = resp.Content.Headers.ContentDisposition;
            if (cd?.FileNameStar is { Length: > 0 } nameStar)
                return nameStar;
            if (cd?.FileName is { Length: > 0 } name)
                return name.Trim('"');
        }
        catch { }
        return null;
    }

    public async Task<(string? ContentType, string? FileName, string? TextContent)> GetPreviewDataAsync(Guid attachmentId)
    {
        try
        {
            using var resp = await Client.GetAsync($"/api/attachments/{attachmentId}");
            resp.EnsureSuccessStatusCode();

            var contentType = resp.Content.Headers.ContentType?.MediaType;
            var cd = resp.Content.Headers.ContentDisposition;
            string? fileName = cd?.FileNameStar is { Length: > 0 } ns ? ns
                : cd?.FileName is { Length: > 0 } n ? n.Trim('"')
                : null;

            var textTypes = new[] { "text/", "application/json", "application/xml", "application/csv" };
            var isText = contentType is not null && textTypes.Any(t => contentType.StartsWith(t, StringComparison.OrdinalIgnoreCase));

            if (isText)
            {
                var text = await resp.Content.ReadAsStringAsync();
                return (contentType, fileName, text);
            }

            return (contentType, fileName, null);
        }
        catch { }
        return (null, null, null);
    }
}
