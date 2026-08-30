using Workbench.Common.Enums;
using Workbench.Common.Models;
using Workbench.Modules.Authorization.Models;
using Workbench.Modules.Issues.Models;
using Workbench.Modules.Projects.Models;

namespace Workbench.Modules.Tags.Models;

public class Tag : IEntity<int>, IBelongsToProject
{
    public int Id { get; set; }
    public required int ProjectId { get; set; }

    public required string Name { get; set; }
    public required string? Description { get; set; }
    public required Color Color { get; set; }
    public Project Project { get; set; } = null!;

    public ICollection<Issue> Issues { get; set; } = [];
}