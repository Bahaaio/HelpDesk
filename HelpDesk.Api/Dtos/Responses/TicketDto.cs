using HelpDesk.Api.Models.Enums;

namespace HelpDesk.Api.Dtos.Responses;

public record TicketDto
{
    public required int Id { get; set; }
    public required string Title { get; set; }
    public required string? Description { get; set; }
    public required Status Status { get; set; }
    public required DateTime CreatedAt { get; set; }
    public required int AuthorId { get; set; }

    public required List<string> Tags { get; set; }
    public required List<Guid> Attachments { get; set; }

    public required int VoteScore { get; set; }
}