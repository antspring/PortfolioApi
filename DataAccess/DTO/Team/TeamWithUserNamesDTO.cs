using System.Text.Json.Serialization;

namespace DataAccess.DTO.Team;

public class TeamWithUserNamesDTO
{
    public TeamWithUserNamesDTO(Models.Project.Team team)
    {
        Id = team.Id;
        Name = team.Name;
        Users = team.Users.Select(user => user.Username).ToList();
    }
    [JsonPropertyName("id")] public int Id { get; set; }
    [JsonPropertyName("name")] public string Name { get; set; }
    [JsonPropertyName("users")] public List<string> Users { get; set; }
}