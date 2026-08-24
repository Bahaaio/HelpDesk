using System.Text.Json.Serialization;

namespace HelpDesk.Modules.Issues.Dtos.Requests;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum IssueSort
{
    Latest,
    Oldest,
    HighestScore,
    LowestScore
}