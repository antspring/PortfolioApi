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
}