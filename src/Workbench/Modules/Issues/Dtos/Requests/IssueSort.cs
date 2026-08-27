using System.Text.Json.Serialization;

namespace Workbench.Modules.Issues.Dtos.Requests;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum IssueSort
{
    Latest,
    Oldest,
    HighestScore,
    LowestScore
}
