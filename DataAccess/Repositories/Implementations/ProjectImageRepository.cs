using DataAccess.Models.Project;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Implementations;

public class ProjectImageRepository(PortfolioDbContext dbContext) : IRepository<ProjectImage>
{
    public void Add(ProjectImage entity)
    {
        dbContext.ProjectImages.Add(entity);
    }

    public void Update(ProjectImage entity)
    {
        dbContext.ProjectImages.Update(entity);
    }

    public ProjectImage GetFirstOrDefault(Func<ProjectImage, bool> query)
    {
        return dbContext.ProjectImages.FirstOrDefault(query);
    }

    public IEnumerable<ProjectImage> GetByQuery(Func<ProjectImage, bool> query)
    {
        return dbContext.ProjectImages.Where(query);
    }

    public void Remove(ProjectImage entity)
    {
        dbContext.ProjectImages.Remove(entity);
    }

    public void ExecuteRemove(int projectId)
    {
        dbContext.ProjectImages.Where(image => image.ProjectId == projectId).ExecuteDelete();
    }

    public void SaveChanges()
    {
        dbContext.SaveChanges();
    }
}