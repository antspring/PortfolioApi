using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DataAccess.Models.User;

public class SocialNetwork
{
    public SocialNetwork(string link, int userId)
    {
        Link = link;
        UserId = userId;
    }

    [Key] public int Id { get; set; }
    [JsonPropertyName("link")] public string Link { get; set; }
    [JsonIgnore] public int UserId { get; set; }
    [JsonIgnore] public User User { get; set; }
}