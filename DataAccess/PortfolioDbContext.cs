using DataAccess.Models.Project;
using DataAccess.Models.Tokens;
using DataAccess.Models.User;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class PortfolioDbContext : DbContext
{
    public PortfolioDbContext(DbContextOptions<PortfolioDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<RefreshSession> RefreshSessions { get; set; }
    public DbSet<SocialNetwork> SocialNetworks { get; set; }
    public DbSet<Education> Education { get; set; }
    public DbSet<Style> Styles { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<ProjectImage> ProjectImages { get; set; }
    public DbSet<Favorite> Favorites { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Team>()
            .HasMany(t => t.Projects)
            .WithOne(p => p.OwnerTeam)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Project>()
            .HasMany(p => p.Images)
            .WithOne(i => i.Project)
            .OnDelete(DeleteBehavior.Cascade);
    }
}