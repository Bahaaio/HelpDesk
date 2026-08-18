using HelpDesk.Api.Dtos.Requests;
using HelpDesk.Api.Dtos.Responses;
using HelpDesk.Api.Models.Enums;
using HelpDesk.Api.Services.Invites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HelpDesk.Api.Controllers;

[Authorize(Roles = Role.Technician)]
[ApiController]
[Route("api/[controller]")]
public class InvitesController : ControllerBase
{
    private readonly IInvitesService _invitesService;

    public InvitesController(IInvitesService invitesService)
    {
        _invitesService = invitesService;
    }

    [HttpPost]
    public async Task<ActionResult<InviteDto>> Create(CreateInviteRequest request) =>
        Ok(await _invitesService.CreateInvite(request));
}