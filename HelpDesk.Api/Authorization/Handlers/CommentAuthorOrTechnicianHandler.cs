using HelpDesk.Api.Authorization.Requirements;
using HelpDesk.Api.Models;
using HelpDesk.Api.Models.Enums;
using HelpDesk.Api.Services;
using Microsoft.AspNetCore.Authorization;

namespace HelpDesk.Api.Authorization.Handlers;

public class CommentAuthorOrTechnicianHandler :
    AuthorizationHandler<CommentAuthorOrTechnicianRequirement, Comment>
{
    private readonly ICurrentUser _user;

    public CommentAuthorOrTechnicianHandler(ICurrentUser user)
    {
        _user = user;
    }

    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        CommentAuthorOrTechnicianRequirement requirement,
        Comment resource)
    {
        if (resource.AuthorId == _user.Id || _user.Role == Role.Technician)
            context.Succeed(requirement);

        return Task.CompletedTask;
    }
}