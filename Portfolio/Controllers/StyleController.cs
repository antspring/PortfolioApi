using System.Security.Claims;
using DataAccess;
using DataAccess.DTO.Style;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Services;

namespace Portfolio.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StyleController(StyleService styleService, PortfolioDbContext dbContext) : ControllerBase
    {
        [HttpGet("get")]
        public IActionResult GetStyle()
        {
            var style = styleService.GetStyle(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value));
            return Ok(style);
        }

        [HttpPut("update")]
        public IActionResult UpdateStyle(StyleDTO style)
        {
            styleService.UpdateStyle(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value), style);
            return Ok();
        }
    }
}