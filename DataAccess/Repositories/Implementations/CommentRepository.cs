using DataAccess.Models.Project;
using DataAccess.Repositories.Interfaces;

namespace DataAccess.Repositories.Implementations;

public class CommentRepository(PortfolioDbContext dbContext) : IRepository<Comment>
{
    public void Add(Comment entity)
    {
        dbContext.Comments.Add(entity);
        dbContext.SaveChanges();
    }

    public void Update(Comment entity)
    {
        dbContext.Comments.Update(entity);
        dbContext.SaveChanges();
    }

    public Comment GetFirstOrDefault(Func<Comment, bool> query)
    {
        return dbContext.Comments.FirstOrDefault(query);
    }

    public IEnumerable<Comment> GetByQuery(Func<Comment, bool> query)
    {
        return dbContext.Comments.Where(query);
    }

    public void Remove(Comment entity)
    {
        dbContext.Comments.Remove(entity);
        dbContext.SaveChanges();
    }
}