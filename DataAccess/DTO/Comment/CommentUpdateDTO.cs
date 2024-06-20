using System.Text.Json.Serialization;

namespace DataAccess.DTO.Comment;

public class CommentUpdateDTO
{
    [JsonPropertyName("id")] public int Id { get; set; }
    [JsonPropertyName("content")] public string Content { get; set; }
}