using DataAccess.Models.Tokens;
using DataAccess.Repositories.Implementations;
using Microsoft.Extensions.Configuration;
using Services.Providers;

namespace Services.Services.Tokens;

public class TokenService(
    RefreshSessionRepository refreshSessionRepository,
    TokenGenerator tokenGenerator,
    IConfiguration configuration)
{
    private readonly ClaimsProvider _claimsProvider = new();

    public (string, string) UpdateTokens(string refreshToken)
    {
        var refreshSession =
            refreshSessionRepository.GetFirstOrDefault(session => session.RefreshToken == refreshToken);
        if (refreshSession == default)
        {
            throw new UnauthorizedAccessException();
        }

        refreshSessionRepository.Remove(refreshSession);

        if (refreshSession.ExpirationDate < DateTime.Now)
        {
            throw new UnauthorizedAccessException();
        }

        var newRefreshToken = tokenGenerator.GenerateRefreshToken();
        SaveRefreshToken(newRefreshToken, refreshSession.UserId);
        var claims = _claimsProvider.GetClaims(refreshSession.User);
        return (tokenGenerator.GenerateAccessToken(claims), newRefreshToken);
    }

    public void SaveRefreshToken(string refreshToken, int userId)
    {
        refreshSessionRepository.Add(new RefreshSession(userId, refreshToken,
            DateTime.Now.AddDays(Convert.ToDouble(configuration["EXPIRES_REFRESH_TOKEN"])).ToUniversalTime()));
    }
}