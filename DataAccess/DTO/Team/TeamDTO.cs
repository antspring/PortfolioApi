using System.Text.Json.Serialization;

namespace DataAccess.DTO.Team;

public class TeamDTO
{
    [JsonPropertyName("name")] public string Name { get; set; }
}