using HelpDesk.Modules.Auth.Enums;
using HelpDesk.Modules.Invites.Dtos;
using HelpDesk.Modules.Invites.Dtos.Requests;
using HelpDesk.Modules.Invites.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HelpDesk.Modules.Invites.Controllers;

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