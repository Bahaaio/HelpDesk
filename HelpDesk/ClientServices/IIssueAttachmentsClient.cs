using HelpDesk.Modules.Attachments.Dtos;
using HelpDesk.Modules.Attachments.Models;
using HelpDesk.Modules.Attachments.Services;
using Microsoft.AspNetCore.Components.Forms;

namespace HelpDesk.ClientServices;

public interface IIssueAttachmentsClient
{
    Task<AttachmentDto> Add(int issueId, IBrowserFile file);
    Task Delete(int issueId, Guid attachmentId);
}