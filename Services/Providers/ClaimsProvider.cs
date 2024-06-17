using System.Security.Claims;
using DataAccess.Models.User;

namespace Services.Providers;

public class ClaimsProvider
{
    public List<Claim> GetClaims(User user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, user.Username),
            new(ClaimTypes.NameIdentifier, user.Id.ToString())
        };

        return claims;
    }
}