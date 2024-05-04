using Microsoft.EntityFrameworkCore;
using Portfolio.Models.Tokens;
using Portfolio.Models.User;

namespace Portfolio;

public class PortfolioDbContext : DbContext
{
    public PortfolioDbContext(DbContextOptions<PortfolioDbContext> options) : base(options)
    {
    }
    
    public DbSet<User> Users { get; set; }
    public DbSet<RefreshSession> RefreshSessions { get; set; }
}