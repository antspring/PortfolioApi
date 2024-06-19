using DataAccess.Models.User;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Implementations;

public class EducationRepository(PortfolioDbContext dbContext) : IRepository<Education>
{
    private IQueryable<Education> _query { get; set; } = dbContext.Education;

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
        return _query.FirstOrDefault(query);
    }

    public IEnumerable<Education> GetByQuery(Func<Education, bool> query)
    {
        return _query.Where(query);
    }

    public void Remove(Education entity)
    {
        dbContext.Education.Remove(entity);
        dbContext.SaveChanges();
    }

    public EducationRepository WithUser()
    {
        _query = _query.Include(education => education.User);
        return this;
    }
}