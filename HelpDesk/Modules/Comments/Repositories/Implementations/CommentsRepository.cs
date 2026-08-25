using HelpDesk.Common.Exceptions;
using HelpDesk.Common.Extensions;
using HelpDesk.Data;
using HelpDesk.Data.Persistence.Implementations;
using HelpDesk.Modules.Comments.Dtos;
using HelpDesk.Modules.Comments.Mappers;
using HelpDesk.Modules.Comments.Models;
using HelpDesk.Modules.Issues.Models;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Modules.Comments.Repositories.Implementations;

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