namespace SoftCorpTask.Entities;

public class SocialNetworkData
{
    public Guid Id { get; set; }
    public string UserName { get; set; } = null!;
    public string Type { get; set; } = null!;
    public DateTimeOffset CreationDate { get; set; }
    public Guid CandidateDataId { get; set; }
    public CandidateData CandidateData { get; set; } = null!;
}