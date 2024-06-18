using DataAccess.DTO.SocialNetwork;
using DataAccess.DTO.User;
using DataAccess.Models.User;
using DataAccess.Repositories.Implementations;

namespace Services.Services;

public class UserService(UserRepository userRepository, SocialNetworkRepository socialNetworkRepository)
{
    public UserProfileDto UpdateUser(int userId, UserUpdateDTO userDto)
    {
        var user = userRepository.GetFirstOrDefault(user => user.Id == userId);
        userRepository.Update(user.Update(userDto));
        return new UserProfileDto(user);
    }

    public void AddSocialNetworks(int userId, SocialNetworkDTO socialNetwork)
    {
        socialNetworkRepository.Add(new SocialNetwork(socialNetwork.Link, userId));
    }
    
    public void RemoveSocialNetwork(int userId, SocialNetworkDTO socialNetwork)
    {
        var user = userRepository.GetUserWithSocialNetworks(user => user.Id == userId);
        var socialNetworkFromDB = user.SocialNetworks.FirstOrDefault(sn => sn.Link == socialNetwork.Link);
        socialNetworkRepository.Remove(socialNetworkFromDB);
    }
}