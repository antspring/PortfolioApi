using System.Text.Json.Serialization;

namespace DataAccess.DTO.SocialNetwork;

public class SocialNetworkDTO
{
    [JsonPropertyName("link")] public string Link { get; set; }
}