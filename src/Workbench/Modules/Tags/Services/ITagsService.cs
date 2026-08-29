using Workbench.Modules.Tags.Dtos;
using Workbench.Modules.Tags.Dtos.Requests;

namespace Workbench.Modules.Tags.Services;

/// <summary>
///     Service interface for managing tags within a project.
/// </summary>
public interface ITagsService
{
    /// <summary>
    ///     Retrieves all tags for a given project.
    /// </summary>
    Task<List<TagDto>> GetAll(int projectId);

    /// <summary>
    ///     Creates a new tag for a given project.
    /// </summary>
    /// <param name="projectId">The projectId to create the tag for.</param>
    /// <param name="request">The tag name and optional description.</param>
    Task<TagDto> Create(int projectId, CreateTagRequest request);

    /// <summary>
    ///     Updates the description of an existing tag.
    /// </summary>
    /// <param name="projectId">The projectId of the tag to update.</param>
    /// <param name="tagName">The tagName of the tag to update.</param>
    /// <param name="request">The updated description.</param>
    Task<TagDto> Update(int projectId, string tagName, UpdateTagRequest request);

    /// <summary>
    ///     Deletes an existing tag from a project.
    /// </summary>
    /// <param name="projectId">The projectId of the tag to delete.</param>
    /// <param name="tagName">The tagName of the tag to delete.</param>
    Task Delete(int projectId, string tagName);
}