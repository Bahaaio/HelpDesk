using HelpDesk.Api.Dtos.Responses;

namespace HelpDesk.Api.Services;

/// <summary>
///     Manages file attachments on resources.
///     Does not manage authorization.
/// </summary>
public interface IAttachmentsService
{
    /// <summary>
    ///     Returns the file stream, content type, and original file name for a given attachment.
    /// </summary>
    /// <param name="attachmentId">The unique ID of the attachment to retrieve.</param>
    Task<AttachmentResult> Get(Guid attachmentId);

    /// <summary>
    ///     Uploads a file attachment to a resource.
    /// </summary>
    /// <param name="resourceId">The ID of the resource to attach the file to.</param>
    /// <param name="file">The file to upload.</param>
    Task<AttachmentDto> Add(int resourceId, IFormFile file);

    /// <summary>
    ///     Deletes an attachment.
    /// </summary>
    /// <param name="attachmentId">The unique ID of the attachment to delete.</param>
    Task Delete(Guid attachmentId);

    /// <summary>
    ///     Deletes all attachments for a resource, including files from storage.
    /// </summary>
    /// <param name="resourceId">The ID of the resource to delete attachments for.</param>
    Task DeleteAll(int resourceId);
}