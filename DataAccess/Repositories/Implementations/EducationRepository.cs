using DataAccess.Models.User;
using DataAccess.Repositories.Interfaces;

namespace DataAccess.Repositories.Implementations;

public class EducationRepository(PortfolioDbContext dbContext) : IRepository<Education>
{
    public void Add(Education entity)
    {
        dbContext.Education.Add(entity);
        dbContext.SaveChanges();
    }

    public void Update(Education entity)
    {
        dbContext.Education.Update(entity);
        dbContext.SaveChanges();
    }

    public Education GetFirstOrDefault(Func<Education, bool> query)
    {
        return dbContext.Education.FirstOrDefault(query);
    }

    public IEnumerable<Education> GetByQuery(Func<Education, bool> query)
    {
        return dbContext.Education.Where(query);
    }

    public void Remove(Education entity)
    {
        dbContext.Education.Remove(entity);
        dbContext.SaveChanges();
    }
}