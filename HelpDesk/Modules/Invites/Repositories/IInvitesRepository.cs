using HelpDesk.Data.Persistence;
using HelpDesk.Modules.Invites.Models;

namespace HelpDesk.Modules.Invites.Repositories;

public interface IInvitesRepository : IRepository<InviteCode, string>
{
}