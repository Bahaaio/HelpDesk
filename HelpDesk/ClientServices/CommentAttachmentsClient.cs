using HelpDesk.Dtos.Responses;
using HelpDesk.Models;
using HelpDesk.Services.Attachments;
using Microsoft.AspNetCore.Components.Forms;

namespace HelpDesk.ClientServices;

public class CommentAttachmentsClient : ICommentAttachmentsClient
{
    private const long MaximumFileSize = 5 * 1024 * 1024;
    private readonly IAttachmentsService<Comment> _attachmentsService;

    public CommentAttachmentsClient(IAttachmentsService<Comment> attachmentsService)
    {
        _attachmentsService = attachmentsService;
    }

    public async Task<List<AttachmentDto>> GetAll(int commentId) =>
        await _attachmentsService.GetAll(commentId);

    public async Task<AttachmentDto> Add(int commentId, IBrowserFile file)
    {
        await using var source = file.OpenReadStream(MaximumFileSize);
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