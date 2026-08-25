using HelpDesk.Data;
using HelpDesk.Data.Persistence.Implementations;
using HelpDesk.Modules.Issues.Dtos;
using HelpDesk.Modules.Issues.Mappers;
using HelpDesk.Modules.Issues.Models;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Modules.Issues.Repositories.Implementations;

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