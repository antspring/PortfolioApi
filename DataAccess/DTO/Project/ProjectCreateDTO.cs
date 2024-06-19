using System.Text.Json.Serialization;

namespace DataAccess.DTO.Project;

public class ProjectCreateDTO
{
    [JsonPropertyName("title")] public string Title { get; set; }
    [JsonPropertyName("description")] public string Description { get; set; }
    [JsonPropertyName("ownerId")] public int OwnerId { get; set; }
}