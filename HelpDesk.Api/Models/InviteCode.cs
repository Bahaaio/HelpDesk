namespace HelpDesk.Api.Models;

public class InviteCode
{
    public required string Code { get; set; }
    public DateTime CreatedAt { get; set; }
    public required DateTime ExpiresAt { get; set; }
}