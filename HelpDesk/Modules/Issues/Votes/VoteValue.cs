using System.Text.Json.Serialization;

namespace HelpDesk.Modules.Issues.Votes;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum VoteValue
{
    Downvote = -1,
    Upvote = 1
}