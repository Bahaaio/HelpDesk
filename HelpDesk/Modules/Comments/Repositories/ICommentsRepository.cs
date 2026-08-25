using HelpDesk.Data.Persistence;
using HelpDesk.Modules.Comments.Dtos;
using HelpDesk.Modules.Comments.Models;

namespace HelpDesk.Modules.Comments.Repositories;

public interface ICommentsRepository : IRepository<Comment, int>
{
    Task<List<CommentDto>> GetAllByIssueIdAsync(int issueId);
}