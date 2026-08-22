using HelpDesk.Dtos.Responses;
using Microsoft.AspNetCore.Components.Forms;

namespace HelpDesk.ClientServices;

public interface ICommentAttachmentsClient
{
    const long MaximumFileSizeBytes = 5 * 1024 * 1024;

    static readonly string[] AllowedExtensions = [".jpg", ".jpeg", ".png", ".webp"];

    Task<List<AttachmentDto>> GetAll(int commentId);
    Task<AttachmentDto> Add(int commentId, IBrowserFile file);
    Task Delete(int commentId, Guid attachmentId);
}
