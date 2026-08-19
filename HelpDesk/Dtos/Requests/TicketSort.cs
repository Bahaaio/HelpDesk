using System.Text.Json.Serialization;

namespace HelpDesk.Dtos.Requests;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TicketSort
{
    Latest,
    Oldest,
    HighestScore,
    LowestScore
}
