using Microsoft.EntityFrameworkCore;
using SoftCorpTask.Contexts;
using SoftCorpTask.Entities;
using SoftCorpTask.Exceptions;
using SoftCorpTask.Mapping;
using SoftCorpTask.Models.Candidates;

namespace SoftCorpTask.Services;

public class CandidateService : ICandidateService
{
    private readonly ApplicationDbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly TimeProvider _timeProvider;
    private readonly CandidateMapper _candidateMapper;

    public CandidateService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor, TimeProvider timeProvider)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
        _timeProvider = timeProvider;
        _candidateMapper = new CandidateMapper();
    }

    public async Task<IEnumerable<CandidateModel>> GetAllCandidatesAsync()
    {
        var workGroupId = GetWorkGroupId();
        var entities = await _context.Candidates
            .Include(x => x.CandidateData)
            .ThenInclude(x => x.SocialNetworks)
            .Where(x => x.WorkGroupId == workGroupId)
            .ToListAsync();
        var models = entities.Select(_candidateMapper.Map);
        return models;
    }

    public async Task<CandidateModel?> CreateCandidateAsync(CreateCandidateModel createCandidateModel)
    {
        var entity = new Candidate
        {
            UpdatedAt = _timeProvider.GetUtcNow(),
            WorkType = createCandidateModel.WorkType,
            WorkGroupId = GetWorkGroupId(),
            CandidateData = new CandidateData
            {
                FirstName = createCandidateModel.FirstName,
                LastName = createCandidateModel.LastName,
                PatronymicName = createCandidateModel.PatronymicName,
                Email = createCandidateModel.Email,
                PhoneNumber = createCandidateModel.PhoneNumber,
                Country = createCandidateModel.Country,
                BirthDate = createCandidateModel.BirthDate
            }
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

    public async Task UpdateCandidateAsync(UpdateCandidateModel updateCandidateModel)
    {
        var workGroupId = GetWorkGroupId();
        
        var entity = await _context.Candidates
            .Include(x => x.CandidateData)
            .ThenInclude(x => x.SocialNetworks)
            .Where(x => x.WorkGroupId == workGroupId && x.Id == updateCandidateModel.Id)
            .FirstOrDefaultAsync();
        if (entity is null)
            throw new CandidateNotFoundException();
        
        entity.WorkType = updateCandidateModel.WorkType;
        entity.WorkGroupId = updateCandidateModel.WorkGroupId;
        entity.CandidateData.FirstName = updateCandidateModel.FirstName;
        entity.CandidateData.LastName = updateCandidateModel.LastName;
        entity.CandidateData.PatronymicName = updateCandidateModel.PatronymicName;
        entity.CandidateData.Email = updateCandidateModel.Email;
        entity.CandidateData.PhoneNumber = updateCandidateModel.PhoneNumber;
        entity.CandidateData.Country = updateCandidateModel.Country;
        entity.CandidateData.BirthDate = updateCandidateModel.BirthDate;
        entity.UpdatedAt = _timeProvider.GetUtcNow();
        
        _context.Update(entity);
        await _context.SaveChangesAsync();
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