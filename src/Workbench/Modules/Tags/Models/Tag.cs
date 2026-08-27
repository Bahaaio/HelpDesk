using Workbench.Common.Models;
using Workbench.Modules.Issues.Models;

namespace Workbench.Modules.Tags.Models;

public class Tag : IEntity<int>

{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }

    public ICollection<Issue> Issues { get; set; } = [];
}
