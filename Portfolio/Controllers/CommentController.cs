using System.Security.Claims;
using DataAccess.DTO.Comment;
using Microsoft.AspNetCore.Mvc;
using Services.Services;

namespace Portfolio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController(CommentService commentService) : ControllerBase
    {
        [HttpPost("add")]
        public IActionResult AddComment(CommentDTO commentDto)
        {
            commentService.AddComment(commentDto, int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value));
            return Ok();
        }
    }
}