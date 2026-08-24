using HelpDesk.Modules.Attachments.Dtos;
using HelpDesk.Modules.Attachments.Models;
using HelpDesk.Modules.Attachments.Services;
using Microsoft.AspNetCore.Components.Forms;

namespace HelpDesk.ClientServices;

public interface ICommentAttachmentsClient
{
    Task<AttachmentDto> Add(int commentId, IBrowserFile file);
    Task Delete(int commentId, Guid attachmentId);
}