using Workbench.Data.Persistence;
using Workbench.Modules.Invites.Models;

namespace Workbench.Modules.Invites.Repositories;

public interface IInvitesRepository : IRepository<InviteCode, string>
{
}
