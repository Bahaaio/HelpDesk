using Workbench.Modules.Auth.Enums;
using Workbench.Modules.Invites.Dtos;
using Workbench.Modules.Invites.Dtos.Requests;
using Workbench.Modules.Invites.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Workbench.Modules.Invites.Controllers;

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
