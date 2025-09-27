namespace SoftCorpTask.Entities;

public class Candidate
{
    public Guid Id { get; set; }
    public Guid WorkGroupId { get; set; }
    public WorkGroup WorkGroup { get; set; } = null!;
}