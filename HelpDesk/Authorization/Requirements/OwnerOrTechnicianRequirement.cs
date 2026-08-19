using Microsoft.AspNetCore.Authorization;

namespace HelpDesk.Authorization.Requirements;

public class OwnerOrTechnicianRequirement : IAuthorizationRequirement;
