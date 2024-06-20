using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DataAccess.Models.Project;

public class ProjectImage
{
    [Key] public int Id { get; set; }
    public string ImagePath { get; set; }
    [JsonIgnore] public Project Project { get; set; }
    [JsonIgnore] public int ProjectId { get; set; }
}