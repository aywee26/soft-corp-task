using System.Text.Json.Serialization;
using SoftCorpTask.Enums;

namespace SoftCorpTask.Models.Candidates;

public class CandidateModel
{
    public Guid Id { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
    public CandidateWorkType WorkType { get; set; }
    public Guid WorkGroupId { get; set; }
    [JsonPropertyName("firstName")] public string CandidateDataFirstName { get; set; } = null!;
    [JsonPropertyName("lastName")] public string CandidateDataLastName { get; set; } = null!;
    [JsonPropertyName("patronymicName")] public string CandidateDataPatronymicName { get; set; } = null!;
    [JsonPropertyName("email")] public string CandidateDataEmail { get; set; } = null!;
    [JsonPropertyName("phoneNumber")] public string CandidateDataPhoneNumber { get; set; } = null!;
    [JsonPropertyName("country")] public string CandidateDataCountry { get; set; } = null!;
    [JsonPropertyName("birthDate")] public DateTimeOffset CandidateDataBirthDate { get; set; }
    
}