using System.Text.Json.Serialization;

namespace DataAccess.DTO.Education;

public class EducationDTO
{
    [JsonPropertyName("id")] public int Id { get; set; }
    [JsonPropertyName("name")] public string Name { get; set; }
    [JsonPropertyName("speciality")] public string? Speciality { get; set; }
    
    public string? FilePath { get; set; }
}