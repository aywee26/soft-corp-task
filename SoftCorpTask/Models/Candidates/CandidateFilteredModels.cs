namespace SoftCorpTask.Models.Candidates;

public class CandidateFilteredModels
{
    public int TotalCount { get; set; }
    public IEnumerable<CandidateModel> Candidates { get; set; } = null!;
}