using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models.Project;

public class Project
{
    [Key] public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public User.User? Owner { get; set; }
    public Team? OwnerTeam { get; set; }
    public DateTime CreatedAt { get; set; }
}