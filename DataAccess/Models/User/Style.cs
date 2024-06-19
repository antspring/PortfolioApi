using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DataAccess.Models.User;

public class Style
{
    [JsonIgnore] [Key] public int Id { get; set; }
    [JsonPropertyName("content")] public string? Content { get; set; }
    [JsonIgnore] public User User { get; set; }
    [JsonIgnore] public int UserId { get; set; }
}