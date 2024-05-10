using DataAccess.Models.Tokens;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Implementations;

public class RefreshSessionRepository(PortfolioDbContext dbContext) : IRepository<RefreshSession>
{
    public void Add(RefreshSession entity)
    {
        dbContext.RefreshSessions.Add(entity);
        dbContext.SaveChanges();
    }

    public void Update(RefreshSession entity)
    {
        dbContext.RefreshSessions.Update(entity);
        dbContext.SaveChanges();
    }

    public RefreshSession GetFirstOrDefault(Func<RefreshSession, bool> query)
    {
        return dbContext.RefreshSessions.Include(session => session.User).FirstOrDefault(query);
    }

    public IEnumerable<RefreshSession> GetByQuery(Func<RefreshSession, bool> query)
    {
        return dbContext.RefreshSessions.Include(session => session.User).Where(query);
    }

    public void Remove(RefreshSession entity)
    {
        dbContext.RefreshSessions.Remove(entity);
        dbContext.SaveChanges();
    }
}