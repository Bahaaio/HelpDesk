using HelpDesk.Data;
using HelpDesk.Data.Persistence.Implementations;
using HelpDesk.Modules.Invites.Models;

namespace HelpDesk.Modules.Invites.Repositories.Implementations;

public class InvitesRepository : Repository<InviteCode, string>, IInvitesRepository
{
    public InvitesRepository(AppDbContext context) : base(context)
    {
    }
}