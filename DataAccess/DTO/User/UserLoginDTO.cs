using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DataAccess.DTO.User;

public class UserLoginDTO
{
    [Required]
    [EmailAddress]
    [JsonPropertyName("email")]
    public string Email { get; set; }

    [Required]
    [JsonPropertyName("password")]
    public string Password { get; set; }
}