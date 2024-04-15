using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Portfolio.Models.User;

namespace Portfolio.Services;

public class AuthenticationService(
    PortfolioDbContext dbContext,
    IHttpContextAccessor httpContextAccessor,
    TokenService tokenService)
{
    public string Registration(User user)
    {
        dbContext.Users.Add(user);
        dbContext.SaveChanges();
        var claims = new List<Claim> { new(ClaimTypes.Name, user.Username) };
        httpContextAccessor.HttpContext?.SignInAsync(new ClaimsPrincipal(new ClaimsIdentity(claims)));
        return tokenService.GenerateAccessToken(claims);
    }

    public string Login(UserLogin user)
    {
        var userFromDb =
            dbContext.Users.FirstOrDefault(u => u.Email == user.Email && u.Password == user.Password);
        if (userFromDb == null) throw new UnauthorizedAccessException();
        var claims = new List<Claim> { new(ClaimTypes.Name, userFromDb.Username) };
        httpContextAccessor.HttpContext?.SignInAsync(new ClaimsPrincipal(new ClaimsIdentity(claims)));
        return tokenService.GenerateAccessToken(claims);
    }
}