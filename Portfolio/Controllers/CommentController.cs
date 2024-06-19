using System.Security.Claims;
using DataAccess.DTO.Comment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Services;

namespace Portfolio.Controllers
{
    [Authorize]
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
        
        [HttpPut("update")]
        public IActionResult UpdateComment(CommentUpdateDTO commentDto)
        {
            commentService.UpdateComment(commentDto, int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value));
            return Ok();
        }
    }
}