using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SoftCorpTask.Controllers;

[ApiController]
[Route("test")]
public class TestController : ControllerBase
{
    [Authorize]
    [HttpGet("auth")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public IActionResult TestAuthentication()
    {
        return NoContent();
    }
}