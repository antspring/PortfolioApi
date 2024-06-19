using System.Security.Claims;
using DataAccess.DTO.Favorite;
using DataAccess.Models.Project;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Services;

namespace Portfolio.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteController(FavoriteService favoriteService) : ControllerBase
    {
        [HttpPost("add")]
        public IActionResult AddFavorite([FromBody] FavoriteDTO favoriteDto)
        {
            favoriteService.AddFavorite(favoriteDto.ProjectId,
                int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value));
            return Ok();
        }

        [HttpGet("get")]
        public IActionResult GetFavorites()
        {
            return Ok(favoriteService.GetFavorites(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)));
        }

        [HttpDelete("remove")]
        public IActionResult RemoveFavorite([FromBody] FavoriteDTO favoriteDto)
        {
            favoriteService.RemoveFavorite(favoriteDto.ProjectId, int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value));
            return Ok();
        }
    }
}