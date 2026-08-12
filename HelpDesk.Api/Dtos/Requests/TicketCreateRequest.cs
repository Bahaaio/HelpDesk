namespace HelpDesk.Api.Dtos.Requests;

public record CreateTicketRequest
{
    public required string Title { get; set; }
    public string? Description { get; set; }
}