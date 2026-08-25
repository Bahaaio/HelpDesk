using HelpDesk.Data.Persistence;
using HelpDesk.Modules.Tags.Dtos;
using HelpDesk.Modules.Tags.Models;

namespace HelpDesk.Modules.Tags.Repositories;

public interface ITagsRepository : IRepository<Tag, int>
{
    /// <summary>Returns all tags projected to DTOs.</summary>
    Task<List<TagDto>> GetAllAsync();

    /// <summary>
    ///     Returns a tracked <see cref="Tag" /> whose name case-insensitively matches
    ///     <paramref name="name" />, or <c>null</c>.
    /// </summary>
    Task<Tag?> FindByNameAsync(string name);

    /// <summary>Returns tags whose names are contained in <paramref name="names" />.</summary>
    Task<List<Tag>> GetByNamesAsync(IEnumerable<string> names);

    /// <summary>
    ///     Bulk-deletes the tag whose name case-insensitively matches <paramref name="name" />.
    /// </summary>
    Task<int> DeleteByNameAsync(string name);
}
