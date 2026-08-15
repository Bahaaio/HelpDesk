using Microsoft.AspNetCore.Authorization;

namespace HelpDesk.Api.Authorization.Requirements;

public class CommentAuthorOrTechnicianRequirement : IAuthorizationRequirement;