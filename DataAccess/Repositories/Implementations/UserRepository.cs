using DataAccess.Models.User;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Implementations;

public class UserRepository(PortfolioDbContext dbContext) : IRepository<User>
{
    public void Add(User entity)
    {
        dbContext.Users.Add(entity);
        dbContext.SaveChanges();
    }

    public void Update(User entity)
    {
        dbContext.Users.Update(entity);
        dbContext.SaveChanges();
    }
    
    public User GetFirstOrDefault(Func<User, bool> query)
    {
        return dbContext.Users.FirstOrDefault(query);
    }

    public IEnumerable<User> GetByQuery(Func<User, bool> query)
    {
        return dbContext.Users.Where(query);
    }
    
    public void Remove(User entity)
    {
        dbContext.Users.Remove(entity);
        dbContext.SaveChanges();
    }
    
    
}