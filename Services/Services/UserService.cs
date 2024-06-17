using DataAccess.DTO.User;
using DataAccess.Repositories.Implementations;

namespace Services.Services;

public class UserService(UserRepository userRepository)
{
    public UserProfileDto UpdateUser(string username, UserUpdateDto userDto)
    {
        var user = userRepository.GetFirstOrDefault(user => user.Username == username);
        userRepository.Update(user.Update(userDto));
        return new UserProfileDto(user);
    }
}