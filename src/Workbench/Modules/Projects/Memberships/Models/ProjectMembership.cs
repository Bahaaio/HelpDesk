using Workbench.Modules.Auth.Models;
using Workbench.Modules.Projects.Enums;
using Workbench.Modules.Projects.Models;

namespace Workbench.Modules.Projects.Memberships.Models;

public class ProjectMembership
{
    /// <summary>
    ///     The role of the user in the project.
    /// </summary>
    public required ProjectMemberRole Role { get; set; }

    public required int UserId { get; set; }
    public ApplicationUser User { get; set; } = null!;

    public required int ProjectId { get; set; }
    public Project Project { get; set; } = null!;
}