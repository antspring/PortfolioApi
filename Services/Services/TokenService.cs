using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Services.Services;

public class TokenService(IConfiguration configuration)
{
    public string GenerateAccessToken(IEnumerable<Claim> claims)
    {
        var jwt = new JwtSecurityToken(
            issuer: configuration["ISSUER"],
            audience: configuration["AUDIENCE"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(configuration["EXPIRES_ACCESS_TOKEN"])),
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                    configuration["SECURITY_KEY"] ?? string.Empty)),
                SecurityAlgorithms.HmacSha256));
        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }

    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
}