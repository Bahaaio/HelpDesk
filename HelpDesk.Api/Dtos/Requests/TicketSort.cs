using System.Text.Json.Serialization;

namespace HelpDesk.Api.Dtos.Requests;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TicketSort
{
    Latest,
    Oldest,
    HighestScore,
    LowestScore
}