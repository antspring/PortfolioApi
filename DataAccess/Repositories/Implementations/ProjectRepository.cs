using DataAccess.Models.Project;
using DataAccess.Repositories.Interfaces;

namespace DataAccess.Repositories.Implementations;

public class ProjectRepository(PortfolioDbContext dbContext) : IRepository<Project>
{
    public void Add(Project entity)
    {
        dbContext.Projects.Add(entity);
        dbContext.SaveChanges();
    }

    public void Update(Project entity)
    {
        dbContext.Projects.Update(entity);
        dbContext.SaveChanges();
    }

    public Project GetFirstOrDefault(Func<Project, bool> query)
    {
        return dbContext.Projects.FirstOrDefault(query);
    }

    public IEnumerable<Project> GetByQuery(Func<Project, bool> query)
    {
        return dbContext.Projects.Where(query);
    }

    public void Remove(Project entity)
    {
        dbContext.Projects.Remove(entity);
    }
}