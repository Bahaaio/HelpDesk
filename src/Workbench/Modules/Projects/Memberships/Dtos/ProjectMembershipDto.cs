using Workbench.Modules.Projects.Enums;

namespace Workbench.Modules.Projects.Memberships.Dtos;

public record ProjectMembershipDto(
    int UserId,
    string Username,
    ProjectMemberRole Role
);