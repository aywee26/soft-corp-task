namespace SoftCorpTask.Models;

public class RefreshTokenModel
{
    public Guid UserId { get; set; }
    public string RefreshToken { get; set; } = null!;
}