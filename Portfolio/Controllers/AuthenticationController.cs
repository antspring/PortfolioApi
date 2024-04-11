using Microsoft.AspNetCore.Mvc;
using Portfolio.Models.User;
using Portfolio.Services;

namespace Portfolio.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController(AuthenticationService authService) : ControllerBase
{
    [HttpPost("registration")]
    public IActionResult Registration(User user)
    {
        return Ok(authService.Registration(user));
    }

    [HttpPost("login")]
    public IActionResult Login(UserLogin user)
    {
        try
        {
            return Ok(authService.Login(user));
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized();
        }
    }
}