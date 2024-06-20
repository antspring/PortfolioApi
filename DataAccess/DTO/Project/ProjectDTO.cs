using System.Text.Json.Serialization;

namespace DataAccess.DTO.Project;

public class ProjectDTO
{
    [JsonPropertyName("title")] public string Title { get; set; }
    [JsonPropertyName("description")] public string Description { get; set; }
    [JsonPropertyName("isTeam")] public bool IsTeam { get; set; }
    [JsonPropertyName("ownerId")] public int OwnerId { get; set; }
}