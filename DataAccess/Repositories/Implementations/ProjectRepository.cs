using DataAccess.Models.Project;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Implementations;

public class ProjectRepository(PortfolioDbContext dbContext) : IRepository<Project>
{
    private IQueryable<Project> _query { get; set; } = dbContext.Projects;

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
        return _query.FirstOrDefault(query);
    }

    public IEnumerable<Project> GetByQuery(Func<Project, bool> query)
    {
        return _query.Where(query);
    }

    public void Remove(Project entity)
    {
        dbContext.Projects.Remove(entity);
        dbContext.SaveChanges();
    }

    public ProjectRepository WithImages()
    {
        _query = _query.Include(project => project.Images);
        return this;
    }
    
    public ProjectRepository WithOwner()
    {
        _query = _query.Include(project => project.Owner);
        return this;
    }
    
    public ProjectRepository WithOwnerTeam()
    {
        _query = _query.Include(project => project.OwnerTeam);
        return this;
    }

    public List<Project> GetAll()
    {
        return _query.ToList();
    }
}