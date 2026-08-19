using HelpDesk.Dtos.Requests;
using HelpDesk.Dtos.Responses;
using HelpDesk.Models.Enums;
using HelpDesk.Services.Invites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HelpDesk.Controllers;

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
