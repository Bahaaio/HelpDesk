using Workbench.Common.Models;

namespace Workbench.Modules.Invites.Models;

public class InviteCode : IEntity<string>
{
    public string Id => Code;
    public required string Code { get; set; }
    public DateTime CreatedAt { get; set; }
    public required DateTime ExpiresAt { get; set; }
}
