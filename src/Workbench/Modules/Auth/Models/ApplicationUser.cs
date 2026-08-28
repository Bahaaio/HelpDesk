using Microsoft.AspNetCore.Identity;
using Workbench.Modules.Comments.Models;
using Workbench.Modules.Issues.Models;
using Workbench.Modules.Issues.Votes.Models;
using Workbench.Modules.Projects.Memberships.Models;
using Workbench.Modules.Projects.Models;

namespace Workbench.Modules.Auth.Models;

public class ApplicationUser : IdentityUser<int>
{
    public ICollection<Issue> CreatedIssues { get; set; } = [];
    public ICollection<Vote> Votes { get; set; } = [];
    public ICollection<Comment> Comments { get; set; } = [];
    public ICollection<Issue> AssignedIssues { get; set; } = [];
    public ICollection<Project> OwnedProjects { get; set; } = [];
    public IEnumerable<ProjectMembership> ProjectMemberships { get; set; } = [];
}