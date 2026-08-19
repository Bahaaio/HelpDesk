using HelpDesk.Dtos.Responses;

namespace HelpDesk.Services.Attachments;

/// <summary>
///     Manages file attachments on resources.
///     Does not manage authorization.
/// </summary>
/// <typeparam name="TOwner">The owner of the attachment.</typeparam>
public interface IAttachmentsService<TOwner>
    where TOwner : class
{
    /// <summary>
    ///     Gets all attachments for a resource.
    /// </summary>
    /// <param name="ownerId">The ID of the resource to get attachments for.</param>
    /// <returns>A list of attachments.</returns>
    Task<List<AttachmentDto>> GetAll(int ownerId);

    /// <summary>
    ///     Uploads a file attachment to a resource.
    /// </summary>
    /// <param name="ownerId">The ID of the resource to attach the file to.</param>
    /// <param name="file">The file to upload.</param>
    Task<AttachmentDto> Add(int ownerId, IFormFile file);

    /// <summary>
    ///     Deletes an attachment.
    /// </summary>
    /// <param name="attachmentId">The unique ID of the attachment to delete.</param>
    Task Delete(Guid attachmentId);

    /// <summary>
    ///     Deletes all attachments for a resource, including files from storage.
    /// </summary>
    /// <param name="ownerId">The ID of the resource to delete attachments for.</param>
    Task DeleteAll(int ownerId);
}
