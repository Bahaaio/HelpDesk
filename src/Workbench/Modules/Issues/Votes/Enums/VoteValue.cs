using System.Text.Json.Serialization;

namespace Workbench.Modules.Issues.Votes.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum VoteValue
{
    Downvote = -1,
    Upvote = 1
}
