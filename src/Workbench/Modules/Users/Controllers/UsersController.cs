using Workbench.Modules.Users.Dtos;
using Workbench.Modules.Users.Services;
using Microsoft.AspNetCore.Mvc;

namespace Workbench.Modules.Users.Controllers;

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
