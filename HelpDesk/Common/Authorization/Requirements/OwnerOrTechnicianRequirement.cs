using Microsoft.AspNetCore.Authorization;

namespace HelpDesk.Common.Authorization.Requirements;

public class OwnerOrTechnicianRequirement : IAuthorizationRequirement;