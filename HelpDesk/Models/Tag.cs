namespace HelpDesk.Models;

public class Tag : IEntity<int>

{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }

    public ICollection<Issue> Issues { get; set; } = [];
}