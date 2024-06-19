using DataAccess;
using DataAccess.DTO.Style;
using DataAccess.Models.User;
using DataAccess.Repositories.Implementations;

namespace Services.Services;

public class StyleService(UserRepository userRepository, StyleRepository styleRepository, PortfolioDbContext dbContext)
{
    public Style GetStyle(int userId)
    {
        var user = userRepository.WithStyles().GetFirstOrDefault(user => userId == user.Id);
        if (user.Style == default)
        {
            styleRepository.Add(new Style { UserId = user.Id });
        }

        return user.Style;
    }

    public void UpdateStyle(int userId, StyleDTO style)
    {
        var styleFromDb = styleRepository.GetFirstOrDefault(s => s.UserId == userId);
        styleFromDb.Content = style.Content;
        styleRepository.Update(styleFromDb);
    }
}