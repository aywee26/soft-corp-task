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
    public DbSet<Candidate> Candidates => Set<Candidate>();
    public DbSet<CandidateData> CandidateDatas => Set<CandidateData>();
    public DbSet<Employee> Employees => Set<Employee>();

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
            e.HasMany(x => x.Candidates).WithOne(x => x.WorkGroup).HasForeignKey(x => x.WorkGroupId).IsRequired();
        });

        modelBuilder.Entity<Candidate>(e =>
        {
            e.ToTable("Candidates");
            e.HasKey(x => x.Id);
        });

        modelBuilder.Entity<CandidateData>(e =>
        {
            e.ToTable("CandidateDatas");
            e.HasKey(x => x.Id);
            e.HasOne(x => x.Candidate).WithOne(x => x.CandidateData).HasForeignKey<CandidateData>(x => x.CandidateId);
        });

        modelBuilder.Entity<Employee>(e =>
        {
            e.ToTable("Employees");
            e.HasKey(x => x.Id);
            e.HasOne(x => x.CandidateData).WithOne(x => x.Employee).HasForeignKey<Employee>(x => x.CandidateDataId);
        });

        modelBuilder.Entity<SocialNetworkData>(e =>
        {
            e.ToTable("SocialNetworkDatas");
            e.HasKey(x => x.Id);
            e.HasOne(x => x.CandidateData).WithMany(x => x.SocialNetworks).HasForeignKey(x => x.CandidateDataId);
        });
    }
}