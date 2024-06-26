﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using DataAccess.DTO.User;
using DataAccess.Models.Project;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Models.User;

[Index(nameof(Email), IsUnique = true)]
[Index(nameof(Username), IsUnique = true)]
public class User : IValidatableObject
{
    [Key] public int Id { get; set; }

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

    [JsonPropertyName("imageUrl")] public string? ImageUrl { get; set; }
    [JsonPropertyName("profession")] public string? Profession { get; set; }
    [JsonPropertyName("location")] public string? Location { get; set; }
    [JsonPropertyName("description")] public string? Description { get; set; }
    public List<SocialNetwork>? SocialNetworks { get; set; }
    public List<Education>? Education { get; set; }
    public Style? Style { get; set; }
    public List<Project.Project>? Projects { get; set; }
    [JsonIgnore] public List<Team>? Teams { get; set; }
    [JsonIgnore] public List<Favorite>? Favorites { get; set; }

    public User Update(UserUpdateDTO userDTO)
    {
        Username = userDTO.Username;
        Profession = userDTO.Profession;
        Location = userDTO.Location;
        Description = userDTO.Description;
        return this;
    }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var dbContext = (PortfolioDbContext)validationContext.GetService(typeof(PortfolioDbContext));
        if (dbContext.Users.Any(user => user.Email == Email))
        {
            yield return new ValidationResult("Email already exists", new[] { nameof(Email) });
        }

        if (dbContext.Users.Any(user => user.Username == Username))
        {
            yield return new ValidationResult("Username already exists", new[] { nameof(Username) });
        }
    }
}
