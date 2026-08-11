namespace HelpDesk.Api.Models;

public class RefreshToken
{
    public int Id { get; set; }
    public required string Hash { get; set; }
    public required DateTime ExpiresAt { get; set; }
    public bool IsRevoked { get; set; }

    public int UserId { get; set; }
    public ApplicationUser User { get; set; } = null!;
}