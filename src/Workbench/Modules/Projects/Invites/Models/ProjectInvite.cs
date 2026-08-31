using Workbench.Common.Models;
using Workbench.Modules.Authorization.Models;

namespace Workbench.Modules.Projects.Invites.Models;

public class ProjectInvite : IEntity<string>, IBelongsToProject, IOwnedByUser
{
    public string Id => Code;
    public int OwnerId => CreatedById;
    public required int ProjectId { get; set; }
    public required string Code { get; set; }
    public required int CreatedById { get; set; }
    public DateTime CreatedAt { get; set; }
    public required DateTime ExpiresAt { get; set; }
}