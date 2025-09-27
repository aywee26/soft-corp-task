using Microsoft.EntityFrameworkCore;
using SoftCorpTask.Contexts;
using SoftCorpTask.Entities;
using SoftCorpTask.Models.Candidates;

namespace SoftCorpTask.Services;

public class CandidateService : ICandidateService
{
    private readonly ApplicationDbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CandidateService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<IEnumerable<CandidateModel>> GetAllCandidatesAsync()
    {
        var workGroupId = GetWorkGroupId();
        var entities = await _context.Candidates.Where(x => x.WorkGroupId == workGroupId).ToListAsync();
        var models = entities.Select(x => new CandidateModel
        {
            Id = x.Id,
            WorkGroupId = x.WorkGroupId
        });
        return models;
    }

    public async Task<CandidateModel?> CreateCandidateAsync()
    {
        var entity = new Candidate
        {
            Id = Guid.NewGuid(),
            WorkGroupId = GetWorkGroupId()
        };
        
        _context.Candidates.Add(entity);
        await _context.SaveChangesAsync();

        var result = new CandidateModel
        {
            Id = entity.Id,
            WorkGroupId = entity.WorkGroupId
        };
        
        return result;
    }

    private Guid GetWorkGroupId()
    {
        var workGroupIdClaim = _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == "WorkGroupId");
        if (workGroupIdClaim is null)
            throw new Exception();
        var id = Guid.Parse(workGroupIdClaim.Value);
        return id;
    }
}