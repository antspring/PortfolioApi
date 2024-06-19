using DataAccess.Models.Project;
using DataAccess.Models.User;
using DataAccess.Repositories.Interfaces;

namespace DataAccess.Repositories.Implementations;

public class TeamRepository(PortfolioDbContext dbContext) : IRepository<Team>
{
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
        return dbContext.Teams.FirstOrDefault(query);
    }

    public IEnumerable<Team> GetByQuery(Func<Team, bool> query)
    {
        return dbContext.Teams.Where(query);
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
}