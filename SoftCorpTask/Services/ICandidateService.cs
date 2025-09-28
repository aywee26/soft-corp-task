using SoftCorpTask.Models.Candidates;

namespace SoftCorpTask.Services;

public interface ICandidateService
{
    Task<CandidateFilteredModels> GetAllCandidatesAsync(CandidateFilterModel filterModel); 
    Task<CandidateModel?> CreateCandidateAsync(CreateCandidateModel createCandidateModel);
    Task UpdateCandidateAsync(UpdateCandidateModel updateCandidateModel);
}