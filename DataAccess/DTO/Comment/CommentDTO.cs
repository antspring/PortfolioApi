using System.Text.Json.Serialization;

namespace DataAccess.DTO.Comment;

public class CommentDTO
{
    [JsonPropertyName("content")] public string Content { get; set; }
    [JsonPropertyName("projectId")] public int ProjectId { get; set; }
}