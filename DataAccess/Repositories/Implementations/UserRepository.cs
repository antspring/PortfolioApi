using DataAccess.Models.User;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace DataAccess.Repositories.Implementations;

public class UserRepository(PortfolioDbContext dbContext) : IRepository<User>
{
    private DbSet<User> Users { get; } = dbContext.Users;

    private IQueryable<User> _query { get; set; } = dbContext.Users;

    public void Add(User entity)
    {
        Users.Add(entity);
        dbContext.SaveChanges();
    }

    public void Update(User entity)
    {
        Users.Update(entity);
        dbContext.SaveChanges();
    }

    public User GetFirstOrDefault(Func<User, bool> query)
    {
        return _query.FirstOrDefault(query);
    }

    public IEnumerable<User> GetByQuery(Func<User, bool> query)
    {
        return _query.Where(query);
    }

    public void Remove(User entity)
    {
        Users.Remove(entity);
        dbContext.SaveChanges();
    }

    public UserRepository WithSocialNetworks()
    {
        _query = _query.Include(user => user.SocialNetworks);
        return this;
    }

    public UserRepository WithEducation()
    {
        _query = _query.Include(user => user.Education);
        return this;
    }

    public UserRepository WithStyles()
    {
        _query = _query.Include(user => user.Style);
        return this;
    }

    public UserRepository WithTeams()
    {
        _query = _query.Include(user => user.Teams);
        return this;
    }
}