using System.ComponentModel.DataAnnotations;

namespace HelpDesk.Modules.Invites;

public record CreateInviteRequest(
    [Range(1, 30)] int ValidDays = 7
);