using Workbench.Modules.Attachments.Dtos;
using Workbench.Modules.Attachments.Models;
using Workbench.Modules.Attachments.Services;
using Microsoft.AspNetCore.Components.Forms;

namespace Workbench.ClientServices;

public interface IIssueAttachmentsClient
{
    Task<AttachmentDto> Add(int issueId, IBrowserFile file);
    Task Delete(int issueId, Guid attachmentId);
}
