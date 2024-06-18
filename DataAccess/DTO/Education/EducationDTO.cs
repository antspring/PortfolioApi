using System.Text.Json.Serialization;

namespace DataAccess.DTO.Education;

public class EducationDTO
{
    [JsonPropertyName("title")] public string Title { get; set; }
    [JsonPropertyName("description")] public string? Description { get; set; }
    [JsonPropertyName("filePath")] public string? FilePath { get; set; }
}