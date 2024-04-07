using Microsoft.AspNetCore.Mvc;
using Portfolio.Models.User;

namespace Portfolio.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthorizationController(PortfolioDbContext context) : ControllerBase
{
    [HttpPost("registration")]
    public IActionResult Index(User user)
    {
        context.Users.Add(user);
        context.SaveChanges();
        return Ok();
    }
}