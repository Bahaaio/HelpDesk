using Workbench.Common.Exceptions;
using Workbench.Data.Persistence;
using Workbench.Modules.Authorization.Extensions;
using Workbench.Modules.Authorization.Services;
using Workbench.Modules.Kanban.Dtos;
using Workbench.Modules.Kanban.Dtos.Requests;
using Workbench.Modules.Kanban.Mappers;
using Workbench.Modules.Kanban.Models;
using Workbench.Modules.Kanban.Repositories;
using Workbench.Modules.Projects.Repositories;

namespace Workbench.Modules.Kanban.Services.Implementations;

public class BoardColumnsService : IBoardColumnsService
{
    private readonly IAuthorizationGuard _authGuard;
    private readonly IBoardsRepository _boardsRepository;
    private readonly IBoardColumnsRepository _columnsRepository;
    private readonly IProjectsRepository _projectsRepository;
    private readonly IUnitOfWork _unitOfWork;

    public BoardColumnsService(
        IBoardsRepository boardsRepository,
        IBoardColumnsRepository columnsRepository,
        IProjectsRepository projectsRepository,
        IUnitOfWork unitOfWork,
        IAuthorizationGuard authGuard)
    {
        _boardsRepository = boardsRepository;
        _columnsRepository = columnsRepository;
        _projectsRepository = projectsRepository;
        _unitOfWork = unitOfWork;
        _authGuard = authGuard;
    }

    public async Task<ColumnDto> Add(int projectId, CreateColumnRequest request)
    {
        await _projectsRepository.ExistsOrThrowAsync(projectId);
        var board = await _boardsRepository.GetByProjectIdRaw(projectId);
        await _authGuard.AuthorizeProjectLead(board);

        var maxPosition = board.Columns.Count > 0 ? board.Columns.Max(c => c.Position) : 0;

        var column = new BoardColumn
        {
            Name = request.Name,
            Description = request.Description,
            Color = request.Color,
            Position = maxPosition + 1,
            BoardId = board.Id
        };

        _columnsRepository.Add(column);
        await _unitOfWork.SaveChangesAsync();

        return column.ToDto();
    }

    public async Task<ColumnDto> Update(int projectId, int columnId, UpdateColumnRequest request)
    {
        var column = await GetColumnForProject(projectId, columnId);

        column.Name = request.Name;
        column.Description = request.Description;
        column.Color = request.Color;

        await _unitOfWork.SaveChangesAsync();

        return column.ToDto();
    }

    public async Task Delete(int projectId, int columnId)
    {
        var column = await GetColumnForProject(projectId, columnId);

        _columnsRepository.Remove(column);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task Reorder(int projectId, List<int> columnIds)
    {
        await _projectsRepository.ExistsOrThrowAsync(projectId);
        var board = await _boardsRepository.GetByProjectIdRaw(projectId);
        await _authGuard.AuthorizeProjectLead(board);

        var columns = board.Columns.ToList();

        for (var i = 0; i < columnIds.Count; i++)
        {
            var column = columns.FirstOrDefault(c => c.Id == columnIds[i]);
            if (column is not null)
                column.Position = i + 1;
        }

        await _unitOfWork.SaveChangesAsync();
    }

    private async Task<BoardColumn> GetColumnForProject(int projectId, int columnId)
    {
        await _projectsRepository.ExistsOrThrowAsync(projectId);
        var board = await _boardsRepository.GetByProjectIdRaw(projectId);
        await _authGuard.AuthorizeProjectLead(board);

        var column = board.Columns.FirstOrDefault(c => c.Id == columnId)
            ?? throw new NotFoundException($"Column {columnId} not found in project {projectId}");

        return column;
    }
}
