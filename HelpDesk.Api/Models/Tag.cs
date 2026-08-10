namespace HelpDesk.Api.Models;

public class Tag
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }

    public ICollection<Ticket> Tickets { get; set; } = [];
}