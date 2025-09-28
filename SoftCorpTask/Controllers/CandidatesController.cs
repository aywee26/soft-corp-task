using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SoftCorpTask.Enums;
using SoftCorpTask.Models.Candidates;
using SoftCorpTask.Services;

namespace SoftCorpTask.Controllers;

[ApiController]
[Route("candidates")]
public class CandidatesController : ControllerBase
{
    private readonly ICandidateService _candidateService;

    public CandidatesController(ICandidateService candidateService)
    {
        _candidateService = candidateService;
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery, Required] int skip,
        [FromQuery, Required] int take,
        [FromQuery] string? firstName,
        [FromQuery] string? lastName,
        [FromQuery] string? patronymicName,
        [FromQuery] string? email,
        [FromQuery] CandidateWorkType[] workTypes,
        [FromQuery] OrderByUpdatedAt? orderBy)
    {
        var filterModel = new CandidateFilterModel()
        {
            Skip = skip,
            Take = take,
            FirstName = firstName,
            LastName = lastName,
            PatronymicName = patronymicName,
            Email = email,
            WorkTypes = workTypes,
            OrderByUpdatedAt = orderBy
        };
        var result = await _candidateService.GetAllCandidatesAsync(filterModel);
        return Ok(result);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCandidateModel createCandidateModel)
    {
        var result = await _candidateService.CreateCandidateAsync(createCandidateModel);
        return Ok(result);
    }

    [Authorize]
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateCandidateModel updateCandidateModel)
    {
        await _candidateService.UpdateCandidateAsync(updateCandidateModel);
        return NoContent();
    }
}