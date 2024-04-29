using Microsoft.AspNetCore.Mvc;
using Portfolio.Models.Tokens;
using Portfolio.Models.User;
using Portfolio.Services;

namespace Portfolio.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController(AuthenticationService authService, IConfiguration configuration) : ControllerBase
{
    [HttpPost("registration")]
    public IActionResult Registration(User user)
    {
        var (accessToken, refreshToken) = authService.Registration(user);
        Response.Cookies.Append("refreshToken", refreshToken, new CookieOptions
        {
            HttpOnly = true,
            SameSite = SameSiteMode.None,
            Secure = true,
            Expires = DateTimeOffset.Now.AddDays(Convert.ToDouble(configuration["EXPIRES_REFRESH_TOKEN"]))
        });
        return Ok(new AccessToken(accessToken));
    }

    [HttpPost("login")]
    public IActionResult Login(UserLogin user)
    {
        try
        {
            var (accessToken, refreshToken) = authService.Login(user);
            Response.Cookies.Append("refreshToken", refreshToken, new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.None,
                Secure = true,
                Expires = DateTimeOffset.Now.AddDays(Convert.ToDouble(configuration["EXPIRES_REFRESH_TOKEN"]))
            });
            return Ok(new AccessToken(accessToken));
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized();
        }
    }
}