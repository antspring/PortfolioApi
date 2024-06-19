using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models.Project;

public class Favorite
{
    public Favorite(int projectId, int userId)
    {
        ProjectId = projectId;
        UserId = userId;
    }

    [Key] public int Id { get; set; }
    public int UserId { get; set; }
    public User.User User { get; set; }
    public int ProjectId { get; set; }
    public Project Project { get; set; }
}