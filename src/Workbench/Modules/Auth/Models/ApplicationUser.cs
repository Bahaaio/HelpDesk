using Workbench.Modules.Comments.Models;
using Workbench.Modules.Issues.Models;
using Workbench.Modules.Issues.Votes.Models;
using Microsoft.AspNetCore.Identity;

namespace Workbench.Modules.Auth.Models;

public class ApplicationUser : IdentityUser<int>
{
    public ICollection<Issue> CreatedIssues { get; set; } = [];
    public ICollection<Vote> Votes { get; set; } = [];
    public ICollection<Comment> Comments { get; set; } = [];
    public ICollection<Issue> AssignedIssues { get; set; } = [];
}
