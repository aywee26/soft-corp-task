using SoftCorpTask.Models.Candidates;

namespace SoftCorpTask.Services;

public interface ICandidateService
{
    Task<IEnumerable<CandidateModel>> GetAllCandidatesAsync(); 
    Task<CandidateModel?> CreateCandidateAsync(CreateCandidateModel createCandidateModel);
    Task UpdateCandidateAsync(UpdateCandidateModel updateCandidateModel);
}