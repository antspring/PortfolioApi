using System.Security.Claims;
using DataAccess;
using DataAccess.Models.Tokens;
using DataAccess.Models.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Services.Services;

public class AuthenticationService(
    PortfolioDbContext dbContext,
    IHttpContextAccessor httpContextAccessor,
    IConfiguration configuration,
    TokenService tokenService)
{
    public (string, string) Registration(User user)
    {
        dbContext.Users.Add(user);
        dbContext.SaveChanges();
        var claims = new List<Claim> { new(ClaimTypes.Name, user.Username) };
        httpContextAccessor.HttpContext?.SignInAsync(new ClaimsPrincipal(new ClaimsIdentity(claims)));
        var refreshToken = tokenService.GenerateRefreshToken();
        SaveRefreshToken(refreshToken, user.Id);
        return (tokenService.GenerateAccessToken(claims), refreshToken);
    }

    public (string, string) Login(UserLogin user)
    {
        var userFromDb =
            dbContext.Users.FirstOrDefault(u => u.Email == user.Email && u.Password == user.Password);
        if (userFromDb == default) throw new UnauthorizedAccessException();
        var claims = new List<Claim> { new(ClaimTypes.Name, userFromDb.Username) };
        httpContextAccessor.HttpContext?.SignInAsync(new ClaimsPrincipal(new ClaimsIdentity(claims)));
        var refreshToken = tokenService.GenerateRefreshToken();
        SaveRefreshToken(refreshToken, userFromDb.Id);
        return (tokenService.GenerateAccessToken(claims), refreshToken);
    }

    public (string, string) UpdateTokens(string refreshToken)
    {
        var refreshSession = dbContext.RefreshSessions
            .Include(refreshSession => refreshSession.User)
            .FirstOrDefault(session => session.RefreshToken == refreshToken);
        if (refreshSession == default)
        {
            throw new UnauthorizedAccessException();
        }

        if (refreshSession.ExpirationDate < DateTime.Now)
        {
            dbContext.RefreshSessions.Remove(refreshSession);
            dbContext.SaveChanges();
            throw new UnauthorizedAccessException();
        }

        dbContext.RefreshSessions.Remove(refreshSession);
        var newRefreshToken = tokenService.GenerateRefreshToken();
        SaveRefreshToken(newRefreshToken, refreshSession.UserId);
        var claims = new List<Claim> { new(ClaimTypes.Name, refreshSession.User.Username) };
        return (tokenService.GenerateAccessToken(claims), newRefreshToken);
    }

    private void SaveRefreshToken(string refreshToken, int userId)
    {
        dbContext.RefreshSessions.Add(new RefreshSession(userId, refreshToken,
            DateTime.Now.AddDays(Convert.ToDouble(configuration["EXPIRES_REFRESH_TOKEN"])).ToUniversalTime()));
        dbContext.SaveChanges();
    }
}