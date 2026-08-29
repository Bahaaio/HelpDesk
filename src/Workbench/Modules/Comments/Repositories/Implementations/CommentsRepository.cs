using Microsoft.EntityFrameworkCore;
using Workbench.Common.Exceptions;
using Workbench.Common.Extensions;
using Workbench.Data;
using Workbench.Data.Persistence.Implementations;
using Workbench.Modules.Comments.Dtos;
using Workbench.Modules.Comments.Mappers;
using Workbench.Modules.Comments.Models;
using Workbench.Modules.Issues.Models;

namespace Workbench.Modules.Comments.Repositories.Implementations;

public class CommentsRepository : Repository<Comment, int>, ICommentsRepository
{
    private readonly DbSet<Issue> _issues;

    public CommentsRepository(AppDbContext context) : base(context)
    {
        _issues = context.Set<Issue>();
    }

    public override async Task<Comment> GetByIdAsync(int id) =>
        await DbSet
            .Where(c => c.Id == id)
            .Include(c => c.Author)
            .Include(c => c.Attachments)
            .Include(c => c.Issue).ThenInclude(i => i.Project)
            .SingleOrDefaultAsync()
        ?? throw new NotFoundException($"Comment with id: {id} not found");

    public async Task<List<CommentDto>> GetAllByIssueIdAsync(int issueId)
    {
        await _issues.ExistsOrThrowAsync(issueId);

        return await DbSet
            .AsNoTracking()
            .Where(c => c.IssueId == issueId)
            .OrderByDescending(c => c.CreatedAt)
            .Select(CommentMapper.ToDtoExpression)
            .ToListAsync();
    }
}