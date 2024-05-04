using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Portfolio.Models.User;

public class UserLogin
{
    [Required]
    [EmailAddress]
    [JsonPropertyName("email")]
    public string Email { get; set; }

    [Required]
    [JsonPropertyName("password")]
    public string Password { get; set; }
}