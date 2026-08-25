using HelpDesk.Data;
using HelpDesk.Data.Persistence.Implementations;
using HelpDesk.Modules.Tags.Dtos;
using HelpDesk.Modules.Tags.Mappers;
using HelpDesk.Modules.Tags.Models;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Modules.Tags.Repositories.Implementations;

public class TagsRepository : Repository<Tag, int>, ITagsRepository
{
    public TagsRepository(AppDbContext context) : base(context)
    {
    }

    public Task<List<TagDto>> GetAllAsync() =>
        DbSet
            .AsNoTracking()
            .Select(TagMapper.ToDtoExpression)
            .ToListAsync();

    public Task<Tag?> FindByNameAsync(string name) =>
        DbSet.SingleOrDefaultAsync(t => EF.Functions.ILike(t.Name, name));

    public Task<List<Tag>> GetByNamesAsync(IEnumerable<string> names) =>
        DbSet.Where(t => names.Contains(t.Name)).ToListAsync();

    public Task<int> DeleteByNameAsync(string name) =>
        DbSet
            .Where(t => EF.Functions.ILike(t.Name, name))
            .ExecuteDeleteAsync();
}