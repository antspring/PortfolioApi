using System.Text.Json.Serialization;

namespace DataAccess.DTO.Project;

public class ProjectDTO
{
    [JsonPropertyName("id")] public int? Id { get; set; }
    [JsonPropertyName("title")] public string Title { get; set; }
    [JsonPropertyName("description")] public string Description { get; set; }
    [JsonPropertyName("isTeam")] public bool IsTeam { get; set; }
    [JsonPropertyName("ownerId")] public int OwnerId { get; set; }
    [JsonPropertyName("link")] public string? Link { get; set; }
}