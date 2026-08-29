using Microsoft.EntityFrameworkCore;
using Workbench.Data;
using Workbench.Modules.Projects.Memberships.Dtos;
using Workbench.Modules.Projects.Memberships.Mappers;
using Workbench.Modules.Projects.Memberships.Models;

namespace Workbench.Modules.Projects.Memberships.Repositories.Implementations;

public class ProjectMembershipsRepository : IProjectMembershipsRepository
{
    private readonly DbSet<ProjectMembership> _dbSet;

    public ProjectMembershipsRepository(AppDbContext dbContext)
    {
        _dbSet = dbContext.Set<ProjectMembership>();
    }

    public Task<ProjectMembership?> GetMembershipByProjectIdAndUserId(int projectId, int userId) =>
        _dbSet
            .Include(pm => pm.User)
            .SingleOrDefaultAsync(pm => pm.ProjectId == projectId && pm.UserId == userId);

    public Task<List<ProjectMembershipDto>> GetMembershipsByProjectId(int projectId) =>
        _dbSet
            .Where(m => m.ProjectId == projectId)
            .Select(ProjectMembershipMapper.ToDtoExpression)
            .ToListAsync();

    public void Add(ProjectMembership membership) => _dbSet.Add(membership);

    public void Remove(ProjectMembership membership) => _dbSet.Remove(membership);
}