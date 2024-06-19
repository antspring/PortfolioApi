using DataAccess.Models.Project;
using DataAccess.Models.User;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Implementations;

public class TeamRepository(PortfolioDbContext dbContext) : IRepository<Team>
{
    private IQueryable<Team> _query { get; set; } = dbContext.Teams;
    public void Add(Team entity)
    {
        dbContext.Teams.Add(entity);
        dbContext.SaveChanges();
    }

    public void Update(Team entity)
    {
        dbContext.Teams.Update(entity);
        dbContext.SaveChanges();
    }

    public Team GetFirstOrDefault(Func<Team, bool> query)
    {
        return _query.FirstOrDefault(query);
    }

    public IEnumerable<Team> GetByQuery(Func<Team, bool> query)
    {
        return _query.Where(query);
    }

    public void Remove(Team entity)
    {
        dbContext.Teams.Remove(entity);
        dbContext.SaveChanges();
    }

    public void AddUserToTeam(Team team, User user)
    {
        team.Users.Add(user);
        dbContext.Teams.Update(team);
        dbContext.SaveChanges();
    }

    public TeamRepository WithUsers()
    {
        _query = _query.Include(team => team.Users);
        return this;
    }
}