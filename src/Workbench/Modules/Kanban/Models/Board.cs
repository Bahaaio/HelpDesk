using Workbench.Common.Models;
using Workbench.Modules.Projects.Models;

namespace Workbench.Modules.Kanban.Models;

public class Board : IEntity<int>
{
    public int Id { get; set; }
    public required string Name { get; set; }

    public required int ProjectId { get; set; }
    public Project Project { get; set; } = null!;

    public ICollection<BoardColumn> Columns { get; set; } = [];
}