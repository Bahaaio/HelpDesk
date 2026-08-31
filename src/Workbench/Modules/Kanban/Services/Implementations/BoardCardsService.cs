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

public class BoardCardsService : IBoardCardsService
{
    private readonly IAuthorizationGuard _authGuard;
    private readonly IBoardsRepository _boardsRepository;
    private readonly IBoardCardsRepository _cardsRepository;
    private readonly IProjectsRepository _projectsRepository;
    private readonly IUnitOfWork _unitOfWork;

    public BoardCardsService(
        IBoardsRepository boardsRepository,
        IBoardCardsRepository cardsRepository,
        IProjectsRepository projectsRepository,
        IUnitOfWork unitOfWork,
        IAuthorizationGuard authGuard)
    {
        _boardsRepository = boardsRepository;
        _cardsRepository = cardsRepository;
        _projectsRepository = projectsRepository;
        _unitOfWork = unitOfWork;
        _authGuard = authGuard;
    }

    public async Task<CardDto> Add(int projectId, CreateCardRequest request)
    {
        await _projectsRepository.ExistsOrThrowAsync(projectId);
        var board = await _boardsRepository.GetByProjectIdRaw(projectId);
        await _authGuard.AuthorizeProjectLead(board);

        if (board.Columns.SelectMany(c => c.Cards).Any(c => c.IssueId == request.IssueId))
            throw new ConflictException($"Issue {request.IssueId} is already on this board");

        var column = board.Columns.FirstOrDefault(c => c.Id == request.ColumnId)
                     ?? throw new NotFoundException(
                         $"Column {request.ColumnId} not found in project {projectId}");

        var maxPosition = column.Cards.Count > 0 ? column.Cards.Max(c => c.Position) : 0;

        var card = new BoardCard
        {
            IssueId = request.IssueId,
            ColumnId = request.ColumnId,
            BoardId = board.Id,
            Position = maxPosition + 1
        };

        _cardsRepository.Add(card);
        await _unitOfWork.SaveChangesAsync();

        await _cardsRepository.LoadIssueAsync(card);
        return card.ToDto();
    }

    public async Task Delete(int projectId, int cardId)
    {
        var project = await _projectsRepository.GetByIdAsync(projectId);
        await _authGuard.AuthorizeProjectLead(project);

        var card = await _cardsRepository.GetByIdAsync(cardId);
        _cardsRepository.Remove(card);

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task Reorder(int projectId, int columnId, MoveCardRequest request)
    {
        await _projectsRepository.ExistsOrThrowAsync(projectId);
        var board = await _boardsRepository.GetByProjectIdRaw(projectId);
        await _authGuard.AuthorizeProjectLead(board);

        var column = board.Columns.FirstOrDefault(c => c.Id == columnId)
                     ?? throw new NotFoundException(
                         $"Column {columnId} not found in project {projectId}");

        var ids = request.CardIds;
        var cards = column.Cards.ToList();

        for (var i = 0; i < ids.Count; i++)
        {
            var card = cards.FirstOrDefault(c => c.Id == ids[i]);
            card?.Position = i + 1;
        }

        await _unitOfWork.SaveChangesAsync();
    }
}