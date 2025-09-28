using SoftCorpTask.Enums;

namespace SoftCorpTask.Models.Candidates;

public class CreateCandidateModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PatronymicName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Country { get; set; }
    public DateTimeOffset BirthDate { get; set; }
    public CandidateWorkType WorkType { get; set; }
}