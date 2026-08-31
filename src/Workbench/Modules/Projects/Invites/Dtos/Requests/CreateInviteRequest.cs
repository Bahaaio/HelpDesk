using System.ComponentModel.DataAnnotations;

namespace Workbench.Modules.Projects.Invites.Dtos.Requests;

public record CreateInviteRequest(
    [Required] int ProjectId,
    [Range(1, 30)] int ValidDays = 7
);