using System.Text.Json.Serialization;

namespace DataAccess.DTO.Comment;

public class CommentGetDTO
{
    [JsonPropertyName("projectId")] public int ProjectId { get; set; }
}