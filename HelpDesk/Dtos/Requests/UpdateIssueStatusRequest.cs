using HelpDesk.Models.Enums;

namespace HelpDesk.Dtos.Requests;

public record UpdateIssueStatusRequest(Status Status);
