using System.Security.Claims;
using DataAccess.DTO.Favorite;
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
        public IActionResult AddFavorite([FromBody] FavoriteCreateDTO favoriteCreateDto)
        {
            favoriteService.AddFavorite(favoriteCreateDto.ProjectId, int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value));
            return Ok();
        }
    }
}