using HelpDesk.Modules.Issues.Enums;

namespace HelpDesk.Modules.Issues.Dtos.Requests;

public record UpdateIssueStatusRequest(Status Status);