using DataAccess.DTO.User;
using DataAccess.Models.Tokens;
using DataAccess.Models.User;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using Services.Services.Tokens;

namespace Portfolio.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController(AuthenticationService authService, TokenService tokenService) : ControllerBase
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
            return StatusCode(422);
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
            var (accessToken, refreshToken) = tokenService.UpdateTokens(Request.Cookies["refreshToken"]);
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