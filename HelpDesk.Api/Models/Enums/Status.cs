using System.Text.Json.Serialization;

namespace HelpDesk.Api.Models.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Status
{
    Open,
    Closed
}