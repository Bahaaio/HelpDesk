using Microsoft.AspNetCore.Identity;

namespace HelpDesk.Api.Models;

public class ApplicationUser : IdentityUser<int>
{
    public ICollection<Ticket> CreatedTickets { get; set; } = [];
    public ICollection<Vote> Votes { get; set; } = [];
    public ICollection<Comment> Comments { get; set; } = [];
    public ICollection<RefreshToken> RefreshTokens { get; set; } = [];
}