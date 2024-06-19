using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using DataAccess.DTO.Education;

namespace DataAccess.Models.User;

public class Education
{
    public Education()
    {
    }

    public Education(EducationDTO education, int userId)
    {
        Title = education.Title;
        Description = education.Description;
        FilePath = education.FilePath;
        UserId = userId;
    }

    [Key] public int Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public string? FilePath { get; set; }
    [JsonIgnore] public int UserId { get; set; }
    [JsonIgnore] public User User { get; set; }
    
    public Education Update(EducationDTO education)
    {
        Title = education.Title;
        Description = education.Description;
        FilePath = education.FilePath;
        return this;
    }
}