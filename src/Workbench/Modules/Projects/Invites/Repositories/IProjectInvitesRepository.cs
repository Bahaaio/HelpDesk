using Workbench.Data.Persistence;
using Workbench.Modules.Projects.Invites.Dtos;
using Workbench.Modules.Projects.Invites.Models;

namespace Workbench.Modules.Projects.Invites.Repositories;

public interface IProjectInvitesRepository : IRepository<ProjectInvite, string>
{
    Task<List<InviteDto>> GetActiveByProjectId(int projectId);
}