using HelpDesk.Dtos.Responses;
using Microsoft.AspNetCore.Components.Forms;

namespace HelpDesk.ClientServices;

public interface IIssueAttachmentsClient
{
    Task<AttachmentDto> Add(int issueId, IBrowserFile file);
    Task Delete(int issueId, Guid attachmentId);
}
