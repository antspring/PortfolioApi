using System.Text.Json.Serialization;
using DataAccess.Models.Project;

namespace DataAccess.DTO.Project;

public class ProjectViewDTO
{
    public ProjectViewDTO(Models.Project.Project project)
    {
        Id = project.Id;
        Title = project.Title;
        Description = project.Description;
        if (project.OwnerId == null)
        {
            Owner = project.OwnerTeam?.Name;
            OwnerId = project.OwnerTeamId;
        }
        else
        {
            Owner = project.Owner?.Username;
            OwnerId = project.OwnerId;
        }

        IsTeam = project.OwnerTeamId != null;
        Images = project.Images;
        Link = project.Link;
        CreatedAt = project.CreatedAt;
    }

    [JsonPropertyName("id")] public int Id { get; set; }
    [JsonPropertyName("title")] public string Title { get; set; }
    [JsonPropertyName("description")] public string Description { get; set; }
    [JsonPropertyName("owner")] public string Owner { get; set; }
    [JsonPropertyName("isTeam")] public bool IsTeam { get; set; }
    [JsonPropertyName("ownerId")] public int? OwnerId { get; set; }
    [JsonPropertyName("images")] public List<ProjectImage>? Images { get; set; }
    [JsonPropertyName("link")] public string? Link { get; set; }
    [JsonPropertyName("createdAt")] public DateTime CreatedAt { get; set; }
}