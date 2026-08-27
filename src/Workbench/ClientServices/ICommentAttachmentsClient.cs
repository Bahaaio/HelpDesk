using Workbench.Modules.Attachments.Dtos;
using Workbench.Modules.Attachments.Models;
using Workbench.Modules.Attachments.Services;
using Microsoft.AspNetCore.Components.Forms;

namespace Workbench.ClientServices;

public interface ICommentAttachmentsClient
{
    Task<AttachmentDto> Add(int commentId, IBrowserFile file);
    Task Delete(int commentId, Guid attachmentId);
}
