using System.Text.Json.Serialization;

namespace DataAccess.DTO.User;

public class UserProfileDto
{
    public UserProfileDto(Models.User.User user)
    {
        Id = user.Id;
        Username = user.Username;
        Email = user.Email;
        ImageUrl = user.ImageUrl;
        Profession = user.Profession;
        Location = user.Location;
        Description = user.Description;
        SocialNetworks = user.SocialNetworks;
        Education = user.Education;
    }

    [JsonPropertyName("id")] public int Id { get; set; }
    [JsonPropertyName("username")] public string Username { get; set; }
    [JsonPropertyName("email")] public string Email { get; set; }
    [JsonPropertyName("imageUrl")] public string? ImageUrl { get; set; }
    [JsonPropertyName("profession")] public string? Profession { get; set; }
    [JsonPropertyName("location")] public string? Location { get; set; }
    [JsonPropertyName("description")] public string? Description { get; set; }
    [JsonPropertyName("socialNetworks")] public List<Models.User.SocialNetwork>? SocialNetworks { get; set; }
    [JsonPropertyName("education")] public List<Models.User.Education>? Education { get; set; }
}