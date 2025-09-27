using SoftCorpTask.Enums;

namespace SoftCorpTask.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Login { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public string PasswordSalt { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public UserRole UserRole { get; set; }
    public Guid? WorkGroupId { get; set; }
    public WorkGroup WorkGroup { get; set; } = null!;
}