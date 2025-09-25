using Microsoft.AspNetCore.Authorization;
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

    [Authorize]
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<UserModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetAllUsers()
    {
        var result = await _usersService.GetAllUsers();
        
        return Ok(result);
    }

    [HttpPost("register")]
    [ProducesResponseType(typeof(UserModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

    [HttpPost("login")]
    [ProducesResponseType(typeof(TokenModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        if (string.IsNullOrWhiteSpace(model.Login)
            || string.IsNullOrWhiteSpace(model.Password))
        {
            return BadRequest();
        }

        var result = await _usersService.LoginAsync(model);
        
        return Ok(result);
    }

    [HttpPost("refresh-token")]
    [ProducesResponseType(typeof(TokenModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenModel model)
    {
        var result = await _usersService.RefreshTokenAsync(model);
        
        return Ok(result);
    }
}