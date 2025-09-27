using Microsoft.EntityFrameworkCore;
using SoftCorpTask.Entities;

namespace SoftCorpTask.Contexts;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    
    public DbSet<User> Users => Set<User>();
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
    public DbSet<WorkGroup> WorkGroups => Set<WorkGroup>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(e =>
        {
            e.ToTable("Users");

            e.HasKey(x => x.Id);
            e.HasIndex(x => x.Login).IsUnique();
        });

        modelBuilder.Entity<RefreshToken>(e =>
        {
            e.ToTable("RefreshTokens");

            e.HasKey(x => x.UserId);
        });

        modelBuilder.Entity<WorkGroup>(e =>
        {
            e.ToTable("WorkGroups");
            e.HasKey(x => x.Id);
            e.HasMany(x => x.Users).WithOne(x => x.WorkGroup).HasForeignKey(x => x.WorkGroupId);
        });
    }
}