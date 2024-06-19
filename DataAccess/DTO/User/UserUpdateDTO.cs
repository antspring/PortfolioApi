using System.Text.Json.Serialization;

namespace DataAccess.DTO.User;

public class UserUpdateDTO
{
    [JsonPropertyName("username")] public string? Username { get; set; }
    [JsonPropertyName("imageUrl")] public string? ImageUrl { get; set; }
    [JsonPropertyName("profession")] public string? Profession { get; set; }
    [JsonPropertyName("location")] public string? Location { get; set; }
    [JsonPropertyName("description")] public string? Description { get; set; }
}