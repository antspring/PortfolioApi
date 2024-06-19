using System.Text.Json.Serialization;

namespace DataAccess.DTO.Style;

public class StyleDTO
{
    [JsonPropertyName("content")] public string Content { get; set; }
}