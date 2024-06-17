using DataAccess.Models.User;
using DataAccess.Repositories.Interfaces;

namespace DataAccess.Repositories.Implementations;

public class SocialNetworkRepository(PortfolioDbContext dbContext) : IRepository<SocialNetwork>
{
    public void Add(SocialNetwork entity)
    {
        dbContext.SocialNetworks.Add(entity);
        dbContext.SaveChanges();
    }

    public void Update(SocialNetwork entity)
    {
        dbContext.SocialNetworks.Update(entity);
        dbContext.SaveChanges();
    }

    public SocialNetwork GetFirstOrDefault(Func<SocialNetwork, bool> query)
    {
        return dbContext.SocialNetworks.FirstOrDefault(query);
    }

    public IEnumerable<SocialNetwork> GetByQuery(Func<SocialNetwork, bool> query)
    {
        return dbContext.SocialNetworks.Where(query);
    }

    public void Remove(SocialNetwork entity)
    {
        dbContext.SocialNetworks.Remove(entity);
        dbContext.SaveChanges();
    }
}