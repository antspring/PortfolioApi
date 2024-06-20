using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using DataAccess.DTO.Comment;

namespace DataAccess.Models.Project;

public class Comment
{
    public Comment()
    {
    }

    public Comment(CommentDTO commentDto, int userId)
    {
        Content = commentDto.Content;
        ProjectId = commentDto.ProjectId;
        UserId = userId;
        CreatedAt = DateTime.Now.ToUniversalTime();
    }

    [Key] public int Id { get; set; }
    public string Content { get; set; }
    [JsonIgnore] public int UserId { get; set; }
    [JsonIgnore] public User.User User { get; set; }
    public int ProjectId { get; set; }
    public Project Project { get; set; }
    public DateTime CreatedAt { get; set; }
}