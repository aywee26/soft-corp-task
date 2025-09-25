namespace SoftCorpTask.Entities;

public class RefreshToken
{
    public Guid UserId { get; set; }
    public string Token { get; set; } = null!;
    public DateTimeOffset ExpiresAt { get; set; }
}