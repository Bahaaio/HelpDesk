using HelpDesk.Dtos.Responses;
using HelpDesk.Models;
using HelpDesk.Options;
using HelpDesk.Services.Attachments;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Options;

namespace HelpDesk.ClientServices;

public class IssueAttachmentsClient : IIssueAttachmentsClient
{
    private readonly IAttachmentsService<Issue> _attachmentsService;
    private readonly IssueAttachmentOptions _options;

    public IssueAttachmentsClient(IAttachmentsService<Issue> attachmentsService,
        IOptions<IssueAttachmentOptions> options)
    {
        _attachmentsService = attachmentsService;
        _options = options.Value;
    }

    public async Task<List<AttachmentDto>> GetAll(int issueId) =>
        await _attachmentsService.GetAll(issueId);

    public async Task<AttachmentDto> Add(int issueId, IBrowserFile file)
    {
        await using var source = file.OpenReadStream(_options.MaxSizeBytes);
        await using var stream = new MemoryStream();
        await source.CopyToAsync(stream);
        stream.Position = 0;

        var formFile = new FormFile(stream, 0, stream.Length, "file", file.Name)
        {
            Headers = new HeaderDictionary(),
            ContentType = file.ContentType ?? "application/octet-stream"
        };

        return await _attachmentsService.Add(issueId, formFile);
    }

    public async Task Delete(int issueId, Guid attachmentId)
    {
        await _attachmentsService.Delete(attachmentId);
    }
}