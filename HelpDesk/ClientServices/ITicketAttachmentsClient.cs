using HelpDesk.Dtos.Responses;
using Microsoft.AspNetCore.Components.Forms;

namespace HelpDesk.ClientServices;

public interface ITicketAttachmentsClient
{
    Task<List<AttachmentDto>> GetAll(int ticketId);
    Task<AttachmentDto> Add(int ticketId, IBrowserFile file);
    Task Delete(int ticketId, Guid attachmentId);
}
