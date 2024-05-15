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
}