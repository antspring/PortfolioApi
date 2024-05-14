using System.Security.Claims;
using DataAccess.Models.User;
using DataAccess.Repositories.Implementations;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Services.Providers;
using Services.Services.Tokens;

namespace Services.Services;

public class AuthenticationService(
    UserRepository userRepository,
    IHttpContextAccessor httpContextAccessor,
    TokenService tokenService,
    TokenGenerator tokenGenerator)
{
    private readonly ClaimsProvider _claimsProvider = new();

    public (string, string) Registration(User user)
    {
        userRepository.Add(user);
        var claims = _claimsProvider.GetClaims(user);
        httpContextAccessor.HttpContext?.SignInAsync(new ClaimsPrincipal(new ClaimsIdentity(claims)));
        var refreshToken = tokenGenerator.GenerateRefreshToken();
        tokenService.SaveRefreshToken(refreshToken, user.Id);
        return (tokenGenerator.GenerateAccessToken(claims), refreshToken);
    }

    public (string, string) Login(UserLogin user)
    {
        var userFromDb =
            userRepository.GetFirstOrDefault(u => u.Email == user.Email && u.Password == user.Password);
        if (userFromDb == default) throw new UnauthorizedAccessException();
        var claims = _claimsProvider.GetClaims(userFromDb);
        httpContextAccessor.HttpContext?.SignInAsync(new ClaimsPrincipal(new ClaimsIdentity(claims)));
        var refreshToken = tokenGenerator.GenerateRefreshToken();
        tokenService.SaveRefreshToken(refreshToken, userFromDb.Id);
        return (tokenGenerator.GenerateAccessToken(claims), refreshToken);
    }
}