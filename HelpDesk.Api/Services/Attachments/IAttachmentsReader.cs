using HelpDesk.Api.Dtos.Responses;

namespace HelpDesk.Api.Services.Attachments;

/// <summary>
///     Read-only interface for attachments.
/// </summary>
public interface IAttachmentsReader
{
    /// <summary>
    ///     Returns the file stream, content type, and original file name for a given attachment.
    /// </summary>
    /// <param name="attachmentId">The unique ID of the attachment to retrieve.</param>
    Task<AttachmentResult> Get(Guid attachmentId);
}