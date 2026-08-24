using HelpDesk.Common.Exceptions;
using HelpDesk.Modules.Issues.Dtos.Requests;
using HelpDesk.Modules.Issues.Models;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Modules.Issues.Extensions;

public static class IssueQueryExtensions
{
    extension(IQueryable<Issue> query)
    {
        /// <summary>
        ///     Applies the filters from the query to the queryable.
        /// </summary>
        /// <param name="issueQuery">The query to apply filters to.</param>
        /// <returns>The filtered query.</returns>
        /// <exception cref="BadRequestException">Thrown if the sort parameter is invalid.</exception>
        public IQueryable<Issue> ApplyFilters(IssueQuery issueQuery)
        {
            if (issueQuery.Status is not null)
                query = query.Where(t => t.Status == issueQuery.Status);

            if (issueQuery.Author is not null)
                query = query.Where(t =>
                    EF.Functions.ILike(t.Author.UserName!, issueQuery.Author)
                );

            if (issueQuery.Tag is not null)
                query = query.Where(t =>
                    t.Tags.Any(tag =>
                        EF.Functions.ILike(tag.Name, issueQuery.Tag))
                );

            if (issueQuery.Q is not null)
            {
                var pattern = $"%{issueQuery.Q}%";
                query = query.Where(t =>
                    EF.Functions.ILike(t.Title, pattern) ||
                    (t.Description != null && EF.Functions.ILike(t.Description, pattern))
                );
            }

            query = issueQuery.Sort switch
            {
                IssueSort.Latest => query.OrderByDescending(t => t.CreatedAt),
                IssueSort.Oldest => query.OrderBy(t => t.CreatedAt),

                IssueSort.HighestScore => query.OrderByDescending(t =>
                    t.Votes.Sum(v => (int)v.Value)),

                IssueSort.LowestScore => query.OrderBy(t =>
                    t.Votes.Sum(v => (int)v.Value)),

                _ => throw new BadRequestException("Invalid sort parameter")
            };

            query = query.AsSplitQuery();

            return query;
        }
    }
}