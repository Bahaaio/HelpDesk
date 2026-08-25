using HelpDesk.Modules.Attachments.Dtos;
using HelpDesk.Modules.Attachments.Models;
using HelpDesk.Modules.Attachments.Services;
using HelpDesk.Modules.Issues.Models;
using HelpDesk.Modules.Issues;
using HelpDesk.Modules.Issues.Options;
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