using HelpDesk.Api.Dtos.Requests;
using HelpDesk.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Api.Extensions;

public static class TicketQueryExtensions
{
    extension(IQueryable<Ticket> query)
    {
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

            if (ticketQuery.Query is not null)
            {
                var pattern = $"%{ticketQuery.Query}%";
                query = query.Where(t =>
                    EF.Functions.ILike(t.Title, pattern) ||
                    (t.Description != null && EF.Functions.ILike(t.Description, pattern))
                );
            }

            return query;
        }
    }
}