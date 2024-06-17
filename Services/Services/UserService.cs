using DataAccess.DTO.SocialNetwork;
using DataAccess.DTO.User;
using DataAccess.Models.User;
using DataAccess.Repositories.Implementations;

namespace Services.Services;

public class UserService(UserRepository userRepository, SocialNetworkRepository socialNetworkRepository)
{
    public UserProfileDto UpdateUser(string username, UserUpdateDto userDto)
    {
        var user = userRepository.GetFirstOrDefault(user => user.Username == username);
        userRepository.Update(user.Update(userDto));
        return new UserProfileDto(user);
    }

    public void AddSocialNetworks(string username, SocialNetworkDTO socialNetwork)
    {
        var user = userRepository.GetFirstOrDefault(user => user.Username == username);
        socialNetworkRepository.Add(new SocialNetwork(socialNetwork.Link, user.Id));
    }
    
    public void RemoveSocialNetwork(string username, SocialNetworkDTO socialNetwork)
    {
        var user = userRepository.GetUserWithSocialNetworks(user => user.Username == username);
        var socialNetworkFromDB = user.SocialNetworks.FirstOrDefault(sn => sn.Link == socialNetwork.Link);
        socialNetworkRepository.Remove(socialNetworkFromDB);
    }
}