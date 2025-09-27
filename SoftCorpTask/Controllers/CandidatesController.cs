using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    public async Task<IActionResult> GetAll()
    {
        var result = await _candidateService.GetAllCandidatesAsync();
        return Ok(result);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create()
    {
        var result = await _candidateService.CreateCandidateAsync();
        return Ok(result);
    }
}