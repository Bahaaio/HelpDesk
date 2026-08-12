namespace HelpDesk.Api.Dtos.Requests;

public record UpdateTicketRequest
{
    public required string Title { get; set; }
    public string? Description { get; set; }
}