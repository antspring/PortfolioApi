using DataAccess.Models.Project;
using DataAccess.Repositories.Implementations;

namespace Services.Services;

public class FavoriteService(FavoriteRepository favoriteRepository)
{
    public void AddFavorite(int projectId, int userId)
    {
        favoriteRepository.Add(new Favorite(projectId, userId));
    }
}