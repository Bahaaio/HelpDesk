using Workbench.Data.Persistence;
using Workbench.Modules.Comments.Dtos;
using Workbench.Modules.Comments.Models;

namespace Workbench.Modules.Comments.Repositories;

public interface ICommentsRepository : IRepository<Comment, int>
{
    Task<List<CommentDto>> GetAllByIssueIdAsync(int issueId);
}
