using System.ComponentModel.DataAnnotations;

namespace Workbench.Modules.Invites.Dtos.Requests;

public record CreateInviteRequest(
    [Range(1, 30)] int ValidDays = 7
);
