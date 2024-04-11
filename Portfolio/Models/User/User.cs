using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Portfolio.Models.User;

public class User
{
    [Key]
    public int Id { get; set; }
    [Required]
    [JsonPropertyName("username")]
    public string Username { get; set; }
    [Required]
    [JsonPropertyName("password")]
    public string Password { get; set; }
    [Required]
    [EmailAddress]
    [JsonPropertyName("email")]
    public string Email { get; set; }
    [JsonPropertyName("imageUrl")]
    public string ImageUrl { get; set; }
}