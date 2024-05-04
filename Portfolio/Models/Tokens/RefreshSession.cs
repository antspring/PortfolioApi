using System.ComponentModel.DataAnnotations;

namespace Portfolio.Models.Tokens;

public record RefreshSession
{
    public RefreshSession(int userId, string refreshToken, DateTime expirationDate)
    {
        UserId = userId;
        RefreshToken = refreshToken;
        ExpirationDate = expirationDate;
    }

    [Key] public int Id { get; set; }
    public int UserId { get; init; }
    public User.User User { get; init; }
    public string RefreshToken { get; init; }
    public DateTime ExpirationDate { get; init; }
};