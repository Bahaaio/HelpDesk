using Workbench.Data;
using Workbench.Data.Persistence.Implementations;
using Workbench.Modules.Issues.Dtos;
using Workbench.Modules.Issues.Mappers;
using Workbench.Modules.Issues.Models;
using Microsoft.EntityFrameworkCore;

namespace Workbench.Modules.Issues.Repositories.Implementations;

public class IssueStatusChangeRepository : Repository<IssueStatusChange, int>,
    IIssueStatusChangeRepository
{
    public IssueStatusChangeRepository(AppDbContext context) : base(context)
    {
    }

    public Task<List<StatusChangeDto>> GetHistoryAsync(int issueId) =>
        DbSet
            .AsNoTracking()
            .Where(s => s.IssueId == issueId)
            .OrderBy(s => s.ChangedAt)
            .Select(StatusChangeMapper.ToDtoExpression)
            .ToListAsync();
}
