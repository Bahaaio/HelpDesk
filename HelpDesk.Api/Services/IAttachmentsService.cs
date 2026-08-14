using HelpDesk.Api.Dtos.Responses;

namespace HelpDesk.Api.Services;

/// <summary>
///     Manages file attachments on tickets. Only the uploader or a technician may delete.
/// </summary>
public interface IAttachmentsService
{
    /// <summary>
    ///     Uploads a file attachment to a ticket. Requires ticket ownership or technician role.
    /// </summary>
    /// <param name="ticketId">The ID of the ticket to attach the file to.</param>
    /// <param name="file">The file to upload.</param>
    Task<AttachmentDto> AddAttachment(int ticketId, IFormFile file);

    /// <summary>
    ///     Deletes an attachment. Only the uploader or a technician may delete.
    /// </summary>
    /// <param name="attachmentId">The unique ID of the attachment to delete.</param>
    Task DeleteAttachment(Guid attachmentId);

    /// <summary>
    ///     Returns the file stream for an attachment.
    /// </summary>
    /// <param name="attachmentId">The unique ID of the attachment to retrieve.</param>
    Task<Stream> GetAttachment(Guid attachmentId);
}