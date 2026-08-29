using Microsoft.EntityFrameworkCore;
using Workbench.Common.Exceptions;
using Workbench.Data;
using Workbench.Data.Persistence.Implementations;
using Workbench.Modules.Issues.Dtos;
using Workbench.Modules.Issues.Mappers;
using Workbench.Modules.Milestones.Dtos;
using Workbench.Modules.Milestones.Mappers;
using Workbench.Modules.Milestones.Models;

namespace Workbench.Modules.Milestones.Repositories.Implementations;

public class MilestonesRepository : Repository<Milestone, int>, IMilestonesRepository
{
    private readonly AppDbContext _context;

    public MilestonesRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<Milestone> GetByIdAsync(int id) =>
        await DbSet
            .Include(m => m.MilestoneItems)
                .ThenInclude(mi => mi.Issue)
            .SingleOrDefaultAsync(m => m.Id == id)
        ?? throw new NotFoundException($"Milestone with id {id} not found");

    public Task<List<MilestoneDto>> GetAllAsync(int projectId) =>
        DbSet
            .Where(m => m.ProjectId == projectId)
            .Include(m => m.MilestoneItems)
                .ThenInclude(mi => mi.Issue)
            .Select(MilestoneMapper.ToDtoExpression)
            .ToListAsync();

    public Task<Milestone?> FindForUpdateAsync(int milestoneId) =>
        DbSet
            .Include(m => m.MilestoneItems)
            .SingleOrDefaultAsync(m => m.Id == milestoneId);

    public Task<Milestone?> FindWithItemsAsync(int milestoneId) =>
        DbSet
            .Include(m => m.MilestoneItems)
                .ThenInclude(mi => mi.Issue)
            .SingleOrDefaultAsync(m => m.Id == milestoneId);

    public Task<List<IssueDto>> GetAllIssuesAsync(int milestoneId) =>
        _context.Set<MilestoneItem>()
            .AsNoTracking()
            .Where(mi => mi.MilestoneId == milestoneId)
            .Select(mi => mi.Issue)
            .Select(IssueMapper.ToDtoExpression)
            .ToListAsync();
}
