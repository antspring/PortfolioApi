using DataAccess.Models.User;
using DataAccess.Repositories.Interfaces;

namespace DataAccess.Repositories.Implementations;

public class StyleRepository(PortfolioDbContext dbContext) : IRepository<Style>
{
    public void Add(Style entity)
    {
        dbContext.Styles.Add(entity);
        dbContext.SaveChanges();
    }

    public void Update(Style entity)
    {
        dbContext.Styles.Update(entity);
        dbContext.SaveChanges();
    }

    public Style GetFirstOrDefault(Func<Style, bool> query)
    {
        return dbContext.Styles.FirstOrDefault(query);
    }

    public IEnumerable<Style> GetByQuery(Func<Style, bool> query)
    {
        return dbContext.Styles.Where(query);
    }

    public void Remove(Style entity)
    {
        dbContext.Styles.Remove(entity);
        dbContext.SaveChanges();
    }
}