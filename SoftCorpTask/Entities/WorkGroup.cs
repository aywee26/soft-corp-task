namespace SoftCorpTask.Entities;

public class WorkGroup
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public ICollection<User> Users { get; set; } = new List<User>();
}