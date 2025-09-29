namespace SoftCorpTask.Models;

public class RegisterUserModel
{
    public required string FullName { get; set; }
    public required string Login { get; set; }
    public required string Password { get; set; }
    public required Guid WorkGroupId { get; set; } 
}