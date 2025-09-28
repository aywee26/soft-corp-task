using System.ComponentModel.DataAnnotations;
using SoftCorpTask.Enums;

namespace SoftCorpTask.Models.Candidates;

public class CandidateFilterModel
{
    [Required] public int Skip { get; set; }
    [Required] public int Take { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PatronymicName { get; set; }
    public string? Email { get; set; }
    public CandidateWorkType[] WorkTypes { get; set; } = null!;
    public OrderByUpdatedAt? OrderByUpdatedAt { get; set; }
}