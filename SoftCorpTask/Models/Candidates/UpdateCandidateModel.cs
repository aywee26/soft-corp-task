using SoftCorpTask.Enums;

namespace SoftCorpTask.Models.Candidates;

public class UpdateCandidateModel
{
    public Guid Id { get; set; }
    public CandidateWorkType WorkType { get; set; }
    public Guid WorkGroupId { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string PatronymicName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string Country { get; set; } = null!;
    public DateTimeOffset BirthDate { get; set; }
}