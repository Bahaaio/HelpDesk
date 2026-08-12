using Microsoft.AspNetCore.Authorization;

namespace HelpDesk.Api.Authorization.Requirements;

public class TicketOwnerOrTechnicianRequirement : IAuthorizationRequirement;