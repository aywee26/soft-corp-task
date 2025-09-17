using SoftCorpTask.Enums;

namespace SoftCorpTask.Models;

public class UserModel
{
    public Guid Id { get; set; }
    public string Login { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public UserRole UserRole { get; set; }
}