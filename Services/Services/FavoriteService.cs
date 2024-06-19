using DataAccess.DTO.Project;
using DataAccess.Models.Project;
using DataAccess.Repositories.Implementations;

namespace Services.Services;

public class FavoriteService(FavoriteRepository favoriteRepository, ProjectRepository projectRepository)
{
    public void AddFavorite(int projectId, int userId)
    {
        favoriteRepository.Add(new Favorite(projectId, userId));
    }

    public IEnumerable<ProjectViewDTO> GetFavorites(int userId)
    {
        var favorites = favoriteRepository.GetFavorites(userId);
        var projects = favorites.Select(f => f.Project).ToList();
        return projects.Select(p => new ProjectViewDTO(p));
    }
}