using System.Text.Json.Serialization;

namespace Workbench.Modules.Issues.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Status
{
    Open,
    InProgress,
    Closed
}
