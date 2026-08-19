using System.Text.Json.Serialization;

namespace HelpDesk.Models.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum VoteValue
{
    Downvote = -1,
    Upvote = 1
}
