using Microsoft.AspNetCore.Mvc;
using SoftCorpTask.Models;
using SoftCorpTask.Services;

namespace SoftCorpTask.Controllers;

[ApiController]
[Route("users")]
public class UsersController : ControllerBase
{
    private readonly IUsersService _usersService;

    public UsersController(IUsersService usersService)
    {
        _usersService = usersService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserModel model)
    {
        if (string.IsNullOrWhiteSpace(model.FullName)
            || string.IsNullOrWhiteSpace(model.Login)
            || string.IsNullOrWhiteSpace(model.Password))
        {
            return BadRequest();
        }
        
        var result = await _usersService.RegisterAsync(model);

        return Ok(result);
    }
}