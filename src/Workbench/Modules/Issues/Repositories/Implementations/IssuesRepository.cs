using Microsoft.EntityFrameworkCore;
using Workbench.Common.Exceptions;
using Workbench.Data;
using Workbench.Data.Persistence.Implementations;
using Workbench.Modules.Issues.Dtos;
using Workbench.Modules.Issues.Dtos.Requests;
using Workbench.Modules.Issues.Extensions;
using Workbench.Modules.Issues.Mappers;
using Workbench.Modules.Issues.Models;

namespace Workbench.Modules.Issues.Repositories.Implementations;

public class IssuesRepository : Repository<Issue, int>, IIssuesRepository
{
    private readonly AppDbContext _context;

    public IssuesRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<Issue> GetByIdAsync(int id) =>
        await DbSet
            .Where(i => i.Id == id)
            .Include(i => i.Project)
            .Include(i => i.Author)
            .Include(i => i.AssignedTo)
            .Include(i => i.Tags)
            .Include(i => i.Attachments)
            .Include(i => i.Votes)
            .AsSplitQuery()
            .SingleOrDefaultAsync()
        ?? throw new NotFoundException($"Issue with id {id} not found");

    public Task<List<IssueDto>> GetAllAsync(int projectId, IssueQuery query) =>
        DbSet
            .AsNoTracking()
            .Where(i => i.ProjectId == projectId)
            .ApplyFilters(query)
            .Select(IssueMapper.ToDtoExpression)
            .ToListAsync();

    public Task<Issue?> FindForUpdateAsync(int id) =>
        DbSet
            .Where(i => i.Id == id)
            .Include(i => i.Author)
            .Include(i => i.AssignedTo)
            .Include(i => i.Tags)
            .Include(i => i.Votes)
            .AsSplitQuery()
            .SingleOrDefaultAsync();

    public Task<Issue?> FindWithTagsAsync(int id) =>
        DbSet
            .Where(i => i.Id == id)
            .Include(i => i.Tags)
            .SingleOrDefaultAsync();

    public Task<List<IssueDto>> GetAllByAuthorAsync(int authorId, IssueQuery query) =>
        DbSet
            .AsNoTracking()
            .ApplyFilters(query)
            .Where(i => i.AuthorId == authorId)
            .Select(IssueMapper.ToDtoExpression)
            .ToListAsync();

    public Task<List<IssueDto>> GetAllAssignedToUserAsync(int userId, IssueQuery query) =>
        DbSet
            .AsNoTracking()
            .ApplyFilters(query)
            .Where(i => i.AssignedToId == userId)
            .Select(IssueMapper.ToDtoExpression)
            .ToListAsync();

    public Task LoadAuthorAsync(Issue issue) =>
        _context.Entry(issue).Reference(i => i.Author).LoadAsync();

    public Task<IssueDto?> FindDtoByIdAsync(int id) =>
        DbSet
            .AsNoTracking()
            .Where(i => i.Id == id)
            .Select(IssueMapper.ToDtoExpression)
            .SingleOrDefaultAsync();
}