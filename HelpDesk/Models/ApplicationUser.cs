using Microsoft.AspNetCore.Identity;

namespace HelpDesk.Models;

public class ApplicationUser : IdentityUser<int>
{
    public ICollection<Issue> CreatedIssues { get; set; } = [];
    public ICollection<Vote> Votes { get; set; } = [];
    public ICollection<Comment> Comments { get; set; } = [];
    public ICollection<Issue> AssignedIssues { get; set; } = [];
}
