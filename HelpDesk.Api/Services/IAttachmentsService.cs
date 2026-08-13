using HelpDesk.Api.Dtos.Responses;

namespace HelpDesk.Api.Services;

public interface IAttachmentsService
{
    Task<AttachmentResponse> AddAttachment(int ticketId, IFormFile file);
    Task DeleteAttachment(Guid attachmentId);
    Task<Stream> GetAttachment(Guid attachmentId);
}