using HelpDesk.Data;
using HelpDesk.Modules.Issues.Votes.Models;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Modules.Issues.Votes.Repositories.Implementations;

public class VotesRepository : IVotesRepository
{
    private readonly DbSet<Vote> _entitySet;

    public VotesRepository(AppDbContext context)
    {
        _entitySet = context.Set<Vote>();
    }

    public async Task<Vote?> FindAsync(int issueId, int userId) =>
        await _entitySet.FindAsync(issueId, userId);

    public void Add(Vote vote) =>
        _entitySet.Add(vote);

    public Task DeleteAsync(int issueId, int userId) =>
        _entitySet
            .Where(v => v.IssueId == issueId && v.VoterId == userId)
            .ExecuteDeleteAsync();
}