using HelpDesk.Modules.Users;
using Microsoft.AspNetCore.Mvc;

namespace HelpDesk.Modules.Users.Controllers;

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