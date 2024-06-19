using DataAccess.DTO.Comment;
using DataAccess.Models.Project;
using DataAccess.Repositories.Implementations;

namespace Services.Services;

public class CommentService(CommentRepository commentRepository)
{
    public void AddComment(CommentDTO commentDto, int userId)
    {
        commentRepository.Add(new Comment(commentDto, userId));
    }

    public void UpdateComment(CommentUpdateDTO commentDto, int userId)
    {
        var comment =
            commentRepository.GetFirstOrDefault(c => c.UserId == userId && c.Id == commentDto.Id);
        comment.Content = commentDto.Content;
        commentRepository.Update(comment);
    }
    
    public void RemoveComment(int commentId, int userId)
    {
        var comment = commentRepository.GetFirstOrDefault(c => c.UserId == userId && c.Id == commentId);
        commentRepository.Remove(comment);
    }
}