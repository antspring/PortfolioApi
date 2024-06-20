using DataAccess.Models.Project;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Implementations;

public class FavoriteRepository(PortfolioDbContext dbContext) : IRepository<Favorite>
{
    public void Add(Favorite entity)
    {
        dbContext.Favorites.Add(entity);
        dbContext.SaveChanges();
    }

    public void Update(Favorite entity)
    {
        dbContext.Favorites.Update(entity);
        dbContext.SaveChanges();
    }

    public Favorite GetFirstOrDefault(Func<Favorite, bool> query)
    {
        return dbContext.Favorites.FirstOrDefault(query);
    }

    public IEnumerable<Favorite> GetByQuery(Func<Favorite, bool> query)
    {
        return dbContext.Favorites.Where(query);
    }

    public List<Favorite> GetFavorites(int userId)
    {
        return dbContext.Favorites
            .Include(f => f.Project.Owner)
            .Include(f => f.Project.OwnerTeam)
            .Include(f => f.Project.Images)
            .ToList();
    }

    public void Remove(Favorite entity)
    {
        dbContext.Favorites.Remove(entity);
        dbContext.SaveChanges();
    }
}