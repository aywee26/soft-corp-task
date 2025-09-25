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
    }
}