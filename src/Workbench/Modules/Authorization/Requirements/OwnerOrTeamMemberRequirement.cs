using Microsoft.AspNetCore.Authorization;

namespace Workbench.Modules.Authorization.Requirements;

public class OwnerOrTeamMemberRequirement : IAuthorizationRequirement
{
}