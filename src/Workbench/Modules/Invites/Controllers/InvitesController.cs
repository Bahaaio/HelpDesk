using Microsoft.AspNetCore.Mvc;
using Workbench.Modules.Invites.Dtos;
using Workbench.Modules.Invites.Dtos.Requests;
using Workbench.Modules.Invites.Services;

namespace Workbench.Modules.Invites.Controllers;

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