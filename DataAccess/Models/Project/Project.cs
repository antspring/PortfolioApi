using System.ComponentModel.DataAnnotations;
using DataAccess.DTO.Project;

namespace DataAccess.Models.Project;

public class Project
{
    [Key] public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public User.User? Owner { get; set; }
    public int? OwnerId { get; set; }
    public Team? OwnerTeam { get; set; }
    public int? OwnerTeamId { get; set; }
    public List<ProjectImage> Images { get; set; }
    public DateTime CreatedAt { get; set; }

    public Project Update(ProjectDTO project)
    {
        Title = project.Title;
        Description = project.Description;
        OwnerId = project.OwnerId;
        return this;
    }
}