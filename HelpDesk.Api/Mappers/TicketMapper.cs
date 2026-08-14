using System.Linq.Expressions;
using HelpDesk.Api.Dtos.Responses;
using HelpDesk.Api.Models;

namespace HelpDesk.Api.Mappers;

public static class TicketMapper
{
    private static readonly Func<Ticket, TicketDto> Compiled = ToDtoExpression.Compile();

    public static Expression<Func<Ticket, TicketDto>> ToDtoExpression => t => new TicketDto
    {
        Id = t.Id,
        Title = t.Title,
        Description = t.Description,
        Status = t.Status,
        CreatedAt = t.CreatedAt,
        AuthorUsername = t.Author.UserName!,
        Tags = t.Tags.Select(tag => tag.Name).ToList(),
        Attachments = t.Attachments.Select(a => a.Id).ToList(),
        VoteScore = t.Votes.Sum(v => (int)v.Value)
    };

    public static TicketDto ToDto(this Ticket t) => Compiled(t);
}