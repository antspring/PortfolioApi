using System.Text.Json.Serialization;

namespace DataAccess.DTO.Project;

public class ProjectRemoveDTO
{
    [JsonPropertyName("id")] public int Id { get; set; }
    [JsonPropertyName("ownerId")] public int OwnerId { get; set; }
    [JsonPropertyName("isTeam")] public bool IsTeam { get; set; }
}