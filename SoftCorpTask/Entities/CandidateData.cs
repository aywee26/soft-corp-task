namespace SoftCorpTask.Entities;

public class CandidateData
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string PatronymicName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string Country { get; set; } = null!;
    public DateTime BirthDate { get; set; }
    public ICollection<SocialNetworkData> SocialNetworks { get; set; } = null!;
    public Guid CandidateId { get; set; }
    public Candidate Candidate { get; set; } = null!;
    public Guid? EmployeeId { get; set; }
    public Employee? Employee { get; set; }
}