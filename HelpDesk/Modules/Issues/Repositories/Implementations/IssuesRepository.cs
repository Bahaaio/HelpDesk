using HelpDesk.Common.Exceptions;
using HelpDesk.Data;
using HelpDesk.Data.Persistence.Implementations;
using HelpDesk.Modules.Issues.Dtos;
using HelpDesk.Modules.Issues.Dtos.Requests;
using HelpDesk.Modules.Issues.Extensions;
using HelpDesk.Modules.Issues.Mappers;
using HelpDesk.Modules.Issues.Models;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Modules.Issues.Repositories.Implementations;

public class IssuesRepository : Repository<Issue, int>, IIssuesRepository
{
    private readonly AppDbContext _context;

    public IssuesRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<Issue> GetByIdAsync(int id) =>
        await DbSet
            .Where(t => t.Id == id)
            .Include(t => t.Author)
            .Include(t => t.AssignedTo)
            .Include(t => t.Tags)
            .Include(t => t.Attachments)
            .Include(t => t.Votes)
            .SingleOrDefaultAsync()
        ?? throw new NotFoundException($"Issue with id {id} not found");

    public Task<List<IssueDto>> GetAllAsync(IssueQuery query) =>
        DbSet
            .AsNoTracking()
            .ApplyFilters(query)
            .Select(IssueMapper.ToDtoExpression)
            .ToListAsync();

    public Task<Issue?> FindForUpdateAsync(int id) =>
        DbSet
            .Where(t => t.Id == id)
            .Include(t => t.Author)
            .Include(t => t.AssignedTo)
            .Include(t => t.Tags)
            .Include(t => t.Votes)
            .AsSplitQuery()
            .SingleOrDefaultAsync();

    public Task<Issue?> FindWithTagsAsync(int id) =>
        DbSet
            .Where(t => t.Id == id)
            .Include(t => t.Tags)
            .SingleOrDefaultAsync();

    public Task<List<IssueDto>> GetAllByAuthorAsync(int authorId, IssueQuery query) =>
        DbSet
            .AsNoTracking()
            .ApplyFilters(query)
            .Where(t => t.AuthorId == authorId)
            .Select(IssueMapper.ToDtoExpression)
            .ToListAsync();

    public Task<List<IssueDto>> GetAllAssignedToUserAsync(int userId, IssueQuery query) =>
        DbSet
            .AsNoTracking()
            .ApplyFilters(query)
            .Where(t => t.AssignedToId == userId)
            .Select(IssueMapper.ToDtoExpression)
            .ToListAsync();

    public Task LoadAuthorAsync(Issue issue) =>
        _context.Entry(issue).Reference(t => t.Author).LoadAsync();

    public Task<IssueDto?> FindDtoByIdAsync(int id) =>
        DbSet
            .AsNoTracking()
            .Where(t => t.Id == id)
            .Select(IssueMapper.ToDtoExpression)
            .SingleOrDefaultAsync();
}