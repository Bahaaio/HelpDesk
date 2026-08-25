using Microsoft.AspNetCore.Authorization;

namespace HelpDesk.Modules.Authorization.Requirements;

public class OwnerOrTechnicianRequirement : IAuthorizationRequirement;