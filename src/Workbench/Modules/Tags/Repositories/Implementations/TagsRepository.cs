using Microsoft.EntityFrameworkCore;
using Workbench.Data;
using Workbench.Data.Persistence.Implementations;
using Workbench.Modules.Tags.Dtos;
using Workbench.Modules.Tags.Mappers;
using Workbench.Modules.Tags.Models;

namespace Workbench.Modules.Tags.Repositories.Implementations;

public class TagsRepository : Repository<Tag, int>, ITagsRepository
{
    public TagsRepository(AppDbContext context) : base(context)
    {
    }

    public Task<List<TagDto>> GetAllByProjectIdAsync(int projectId) =>
        DbSet
            .AsNoTracking()
            .Where(t => t.ProjectId == projectId)
            .Select(TagMapper.ToDtoExpression)
            .ToListAsync();

    public Task<Tag?> FindByNameAsync(int projectId, string name) =>
        DbSet
            .Where(t => t.ProjectId == projectId)
            .SingleOrDefaultAsync(t => EF.Functions.ILike(t.Name, name));

    public Task<List<Tag>> GetByNamesAsync(int projectId, IEnumerable<string> names) =>
        DbSet
            .Where(t => t.ProjectId == projectId)
            .Where(t => names.Contains(t.Name))
            .ToListAsync();

    public Task<int> DeleteByNameAsync(int projectId, string name) =>
        DbSet
            .Where(t => t.ProjectId == projectId)
            .Where(t => EF.Functions.ILike(t.Name, name))
            .ExecuteDeleteAsync();
}