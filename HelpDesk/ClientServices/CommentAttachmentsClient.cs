using HelpDesk.Dtos.Responses;
using HelpDesk.Models;
using HelpDesk.Options;
using HelpDesk.Services.Attachments;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Options;

namespace HelpDesk.ClientServices;

public class CommentAttachmentsClient : ICommentAttachmentsClient
{
    private readonly IAttachmentsService<Comment> _attachmentsService;
    private readonly CommentAttachmentOptions _options;

    public CommentAttachmentsClient(IAttachmentsService<Comment> attachmentsService,
        IOptions<CommentAttachmentOptions> options)
    {
        _attachmentsService = attachmentsService;
        _options = options.Value;
    }

    public async Task<List<AttachmentDto>> GetAll(int commentId) =>
        await _attachmentsService.GetAll(commentId);

    public async Task<AttachmentDto> Add(int commentId, IBrowserFile file)
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

        return await _attachmentsService.Add(commentId, formFile);
    }

    public async Task Delete(int commentId, Guid attachmentId)
    {
        await _attachmentsService.Delete(attachmentId);
    }
}