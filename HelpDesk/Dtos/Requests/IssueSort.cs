using System.Text.Json.Serialization;

namespace HelpDesk.Dtos.Requests;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum IssueSort
{
    Latest,
    Oldest,
    HighestScore,
    LowestScore
}
