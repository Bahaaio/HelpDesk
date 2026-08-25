using HelpDesk.Modules.Issues.Votes.Enums;
using HelpDesk.Modules.Issues.Votes.Models;

namespace HelpDesk.Modules.Issues.Votes.Dtos.Requests;

public record VoteRequest(VoteValue Vote);