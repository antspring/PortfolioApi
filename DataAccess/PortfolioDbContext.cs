using DataAccess.Models.Tokens;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class PortfolioDbContext : DbContext
{
    public PortfolioDbContext(DbContextOptions<PortfolioDbContext> options) : base(options)
    {
    }
    
    public DbSet<Models.User.User> Users { get; set; }
    public DbSet<RefreshSession> RefreshSessions { get; set; }
}