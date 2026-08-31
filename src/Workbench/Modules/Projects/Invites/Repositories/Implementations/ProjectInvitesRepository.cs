using Microsoft.EntityFrameworkCore;
using Workbench.Data;
using Workbench.Data.Persistence.Implementations;
using Workbench.Modules.Projects.Invites.Dtos;
using Workbench.Modules.Projects.Invites.Models;

namespace Workbench.Modules.Projects.Invites.Repositories.Implementations;

public class ProjectInvitesRepository : Repository<ProjectInvite, string>, IProjectInvitesRepository
{
    public ProjectInvitesRepository(AppDbContext context) : base(context)
    {
    }

    public Task<List<InviteDto>> GetActiveByProjectId(int projectId) =>
        DbSet
            .Where(i => i.ProjectId == projectId && i.ExpiresAt > DateTime.UtcNow)
            .Select(i => new InviteDto(i.Code, i.ExpiresAt))
            .ToListAsync();
}