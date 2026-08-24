using HelpDesk.Common.Entities;

namespace HelpDesk.Modules.Invites;

public class InviteCode : IEntity<string>
{
    public string Id => Code;
    public required string Code { get; set; }
    public DateTime CreatedAt { get; set; }
    public required DateTime ExpiresAt { get; set; }
}