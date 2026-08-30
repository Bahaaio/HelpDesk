using Workbench.Modules.Kanban.Dtos;
using Workbench.Modules.Kanban.Dtos.Requests;

namespace Workbench.Modules.Kanban.Services;

public interface IBoardColumnsService
{
    Task<ColumnDto> Add(int projectId, CreateColumnRequest request);
    Task<ColumnDto> Update(int projectId, int columnId, UpdateColumnRequest request);
    Task Delete(int projectId, int columnId);
    Task Reorder(int projectId, List<int> columnIds);
}
