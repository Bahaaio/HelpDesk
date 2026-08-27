using Workbench.Data;
using Workbench.Data.Persistence.Implementations;
using Workbench.Modules.Invites.Models;

namespace Workbench.Modules.Invites.Repositories.Implementations;

public class InvitesRepository : Repository<InviteCode, string>, IInvitesRepository
{
    public InvitesRepository(AppDbContext context) : base(context)
    {
    }
}
