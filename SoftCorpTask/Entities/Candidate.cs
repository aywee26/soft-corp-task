using SoftCorpTask.Enums;

namespace SoftCorpTask.Entities;

public class Candidate
{
    public Guid Id { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
    public CandidateWorkType WorkType { get; set; }
    public Guid CandidateDataId { get; set; }
    public CandidateData CandidateData { get; set; } = null!;
    public Guid WorkGroupId { get; set; }
    public WorkGroup WorkGroup { get; set; } = null!;
}