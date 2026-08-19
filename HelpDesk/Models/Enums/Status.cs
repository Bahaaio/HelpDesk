using System.Text.Json.Serialization;

namespace HelpDesk.Models.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Status
{
    Open,
    Closed
}
