using System.Text.Json.Serialization;

namespace Workbench.Common.Enums;

/// <summary>
///     Represents a color that can be used for visual distinction in the UI.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Color
{
    Gray,
    Red,
    Green,
    Blue,
    Orange,
    Purple
}