using System.Text.Json.Serialization;

namespace HelpDesk.Modules.Issues.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Status
{
    Open,
    Closed
}