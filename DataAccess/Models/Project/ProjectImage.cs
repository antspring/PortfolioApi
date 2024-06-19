using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models.Project;

public class ProjectImage
{
    [Key] public int Id { get; set; }
    public string ImagePath { get; set; }
    public Project Project { get; set; }
    public int ProjectId { get; set; }
}