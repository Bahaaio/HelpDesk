using HelpDesk.Modules.Tags.Dtos;
using HelpDesk.Modules.Tags.Dtos.Requests;

namespace HelpDesk.Modules.Tags.Services;

/// <summary>
///     Manages issue category tags. Only technicians may create or update tags.
/// </summary>
public interface ITagsService
{
    /// <summary>
    ///     Returns all available tags.
    /// </summary>
    Task<List<TagDto>> GetAll();

    /// <summary>
    ///     Creates a new tag. Requires technician role.
    /// </summary>
    /// <param name="request">The tag name and optional description.</param>
    Task<TagDto> Create(CreateTagRequest request);

    /// <summary>
    ///     Updates an existing tag's description. Requires technician role.
    /// </summary>
    /// <param name="name">The name of the tag to update.</param>
    /// <param name="request">The updated description.</param>
    Task<TagDto> Update(string name, UpdateTagRequest request);

    /// <summary>
    ///     Deletes a tag by name. Requires technician role.
    /// </summary>
    /// <param name="name">The name of the tag to delete.</param>
    Task Delete(string name);
}