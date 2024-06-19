using System.Security.Claims;
using DataAccess;
using DataAccess.Models.User;
using DataAccess.Repositories.Implementations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Portfolio.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StyleController(UserRepository userRepository, PortfolioDbContext dbContext) : ControllerBase
    {
        [HttpGet("get")]
        public IActionResult GetStyle()
        {
            var user = userRepository.WithStyles().GetFirstOrDefault(user =>
                int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value) == user.Id);
            if (user.Style == default)
            {
                dbContext.Styles.Add(new Style { UserId = user.Id });
                dbContext.SaveChanges();
            }

            return Ok(user.Style);
        }
    }
}