using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;
using Portfolio.Models.User;

namespace Portfolio.Services;

public class AuthenticationService(PortfolioDbContext dbContext, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
{
    public string Registration(User user)
    {
        dbContext.Users.Add(user);
        dbContext.SaveChanges();
        var claims = new List<Claim> { new(ClaimTypes.Name, user.Username) };
        httpContextAccessor.HttpContext?.SignInAsync(new ClaimsPrincipal(new ClaimsIdentity(claims)));
        var jwt = CreateJwtToken(claims);
        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }

    public string Login(UserLogin user)
    {
        var userFromDb =
            dbContext.Users.FirstOrDefault(u => u.Email == user.Email && u.Password == user.Password);
        if (userFromDb == null) throw new UnauthorizedAccessException();
        var claims = new List<Claim> { new(ClaimTypes.Name, userFromDb.Username) };
        httpContextAccessor.HttpContext?.SignInAsync(new ClaimsPrincipal(new ClaimsIdentity(claims)));
        var jwt = CreateJwtToken(claims);
        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }

    private JwtSecurityToken CreateJwtToken(IEnumerable<Claim> claims)
    {
        return new JwtSecurityToken(
            issuer: configuration["ISSUER"],
            audience: configuration["AUDIENCE"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                    configuration["SECURITY_KEY"] ?? string.Empty)),
                SecurityAlgorithms.HmacSha256));
    }
}