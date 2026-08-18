using HelpDesk.Api.Dtos.Responses;
using HelpDesk.Api.Services.Users;
using Microsoft.AspNetCore.Mvc;

namespace HelpDesk.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUsersService _usersService;

    public UsersController(IUsersService usersService)
    {
        _usersService = usersService;
    }

    [HttpGet("me")]
    public ActionResult<UserDto> GetCurrentUser() => Ok(_usersService.GetCurrentUser());
}