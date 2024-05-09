using DataAccess.Models.Tokens;
using DataAccess.Models.User;
using Microsoft.AspNetCore.Mvc;
using Services.Services;

namespace Portfolio.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController(AuthenticationService authService) : ControllerBase
{
    [HttpPost("registration")]
    public IActionResult Registration(User user)
    {
        var (accessToken, refreshToken) = authService.Registration(user);
        Response.Cookies.Append("refreshToken", refreshToken, new CookieOptions
        {
            HttpOnly = true,
            SameSite = SameSiteMode.None,
            Secure = true
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
                Secure = true
            });
            return Ok(new AccessToken(accessToken));
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized();
        }
    }

    [HttpGet("refresh-tokens")]
    public IActionResult UpdateTokens()
    {
        if (!Request.Cookies.Keys.Contains("refreshToken"))
        {
            return Unauthorized();
        }

        try
        {
            var (accessToken, refreshToken) = authService.UpdateTokens(Request.Cookies["refreshToken"]);
            Response.Cookies.Delete("refreshToken");
            Response.Cookies.Append("refreshToken", refreshToken, new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.None,
                Secure = true
            });

            return Ok(new AccessToken(accessToken));
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized();
        }
    }
}