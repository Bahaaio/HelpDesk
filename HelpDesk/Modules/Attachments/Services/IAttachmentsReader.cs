using HelpDesk.Modules.Attachments.Dtos;

namespace HelpDesk.Modules.Attachments.Services;

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