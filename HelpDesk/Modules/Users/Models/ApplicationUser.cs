using HelpDesk.Modules.Comments.Models;
using HelpDesk.Modules.Issues.Models;
using HelpDesk.Modules.Issues.Votes;
using Microsoft.AspNetCore.Identity;

namespace HelpDesk.Modules.Users.Models;

public class ApplicationUser : IdentityUser<int>
{
    public ICollection<Issue> CreatedIssues { get; set; } = [];
    public ICollection<Vote> Votes { get; set; } = [];
    public ICollection<Comment> Comments { get; set; } = [];
    public ICollection<Issue> AssignedIssues { get; set; } = [];
}