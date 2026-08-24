using HelpDesk.Modules.Attachments.Dtos;

namespace HelpDesk.Modules.Attachments.Services;

/// <summary>
///     Manages file attachments on resources.
///     Does not manage authorization.
/// </summary>
/// <typeparam name="TParent">The type of the resource that owns attachments.</typeparam>
public interface IAttachmentsService<TParent>
    where TParent : class
{
    /// <summary>
    ///     Uploads a file attachment to a resource.
    /// </summary>
    /// <param name="parentId">The ID of the resource to attach the file to.</param>
    /// <param name="file">The file to upload.</param>
    Task<AttachmentDto> Add(int parentId, IFormFile file);

    /// <summary>
    ///     Deletes an attachment.
    /// </summary>
    /// <param name="attachmentId">The unique ID of the attachment to delete.</param>
    Task Delete(Guid attachmentId);

    /// <summary>
    ///     Deletes all attachments for a resource, including files from storage.
    /// </summary>
    /// <param name="parentId">The ID of the resource to delete attachments for.</param>
    Task DeleteAll(int parentId);
}