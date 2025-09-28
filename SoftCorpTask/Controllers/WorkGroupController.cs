using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SoftCorpTask.Enums;
using SoftCorpTask.Models;
using SoftCorpTask.Services;

namespace SoftCorpTask.Controllers;

[ApiController]
[Route("work-groups")]
public class WorkGroupController : ControllerBase
{
    private readonly IWorkGroupService _workGroupService;

    public WorkGroupController(IWorkGroupService workGroupService)
    {
        _workGroupService = workGroupService;
    }

    [Authorize]
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<WorkGroupModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var result = await _workGroupService.GetWorkGroupsAsync();
        return Ok(result);
    }

    [Authorize]
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(WorkGroupModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var result = await _workGroupService.GetWorkGroupByIdAsync(id);
        return result is not null ? Ok(result) : NotFound(result);
    }

    [Authorize("Administrator")]
    [HttpPost]
    [ProducesResponseType(typeof(WorkGroupModel), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateWorkGroupModel model)
    {
        if (string.IsNullOrWhiteSpace(model.Name))
            return BadRequest();
        
        var result = await _workGroupService.CreateWorkGroupAsync(model);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }
}