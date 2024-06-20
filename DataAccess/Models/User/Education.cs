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
        FilePath = education.FilePath;
        Name = education.Name;
        Speciality = education.Speciality;
        UserId = userId;
    }

    [Key] public int Id { get; set; }
    public string Name { get; set; }
    public string? Speciality { get; set; }
    public string? FilePath { get; set; }
    [JsonIgnore] public int UserId { get; set; }
    [JsonIgnore] public User User { get; set; }

    public Education Update(EducationDTO education)
    {
        Name = education.Name;
        Speciality = education.Speciality;
        FilePath = education.FilePath;
        return this;
    }
}