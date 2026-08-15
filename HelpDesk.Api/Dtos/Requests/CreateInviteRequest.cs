using System.ComponentModel.DataAnnotations;

namespace HelpDesk.Api.Dtos.Requests;

public record CreateInviteRequest(
    [Range(1, 30)] int ValidDays = 7
);