using System.Text.Json.Serialization;

namespace HelpDesk.Api.Models.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum VoteValue
{
    Downvote = -1,
    Upvote = 1
}