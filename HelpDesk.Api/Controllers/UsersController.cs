using HelpDesk.Api.Dtos.Responses;
using HelpDesk.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace HelpDesk.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController(UsersService usersService) : ControllerBase
{
    [HttpGet("me")]
    public ActionResult<UserDto> GetCurrentUser()
    {
        return Ok(usersService.GetCurrentUser(User));
    }
}