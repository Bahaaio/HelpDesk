using HelpDesk.Dtos.Requests;
using HelpDesk.Exceptions;
using HelpDesk.Models;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Extensions;

public static class TicketQueryExtensions
{
    extension(IQueryable<Ticket> query)
    {
        /// <summary>
        ///     Applies the filters from the query to the queryable.
        /// </summary>
        /// <param name="ticketQuery">The query to apply filters to.</param>
        /// <returns>The filtered query.</returns>
        /// <exception cref="BadRequestException">Thrown if the sort parameter is invalid.</exception>
        public IQueryable<Ticket> ApplyFilters(TicketQuery ticketQuery)
        {
            if (ticketQuery.Status is not null)
                query = query.Where(t => t.Status == ticketQuery.Status);

            if (ticketQuery.Author is not null)
                query = query.Where(t =>
                    EF.Functions.ILike(t.Author.UserName!, ticketQuery.Author)
                );

            if (ticketQuery.Tag is not null)
                query = query.Where(t =>
                    t.Tags.Any(tag =>
                        EF.Functions.ILike(tag.Name, ticketQuery.Tag))
                );

            if (ticketQuery.Q is not null)
            {
                var pattern = $"%{ticketQuery.Q}%";
                query = query.Where(t =>
                    EF.Functions.ILike(t.Title, pattern) ||
                    (t.Description != null && EF.Functions.ILike(t.Description, pattern))
                );
            }

            query = ticketQuery.Sort switch
            {
                TicketSort.Latest => query.OrderByDescending(t => t.CreatedAt),
                TicketSort.Oldest => query.OrderBy(t => t.CreatedAt),

                TicketSort.HighestScore => query.OrderByDescending(t =>
                    t.Votes.Sum(v => (int)v.Value)),

                TicketSort.LowestScore => query.OrderBy(t =>
                    t.Votes.Sum(v => (int)v.Value)),

                _ => throw new BadRequestException("Invalid sort parameter")
            };

            query = query.AsSplitQuery();

            return query;
        }
    }
}