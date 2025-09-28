namespace SoftCorpTask.Entities;

public class Employee
{
    public Guid Id { get; set; }
    public DateTimeOffset HiredAt { get; set; }
    public Guid CandidateDataId { get; set; }
    public CandidateData CandidateData { get; set; } = null!;
}