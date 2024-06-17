using DataAccess.DTO.SocialNetwork;
using DataAccess.DTO.User;
using DataAccess.Repositories.Implementations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Services;

namespace Portfolio.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(UserRepository userRepository, UserService userService) : ControllerBase
    {
        [HttpGet("get")]
        public IActionResult GetUser()
        {
            var user = userRepository.GetUserWithSocialNetworks(user => User.Identity.Name == user.Username);
            return Ok(new UserProfileDto(user));
        }

        [HttpPut("update")]
        public IActionResult UpdateUser(UserUpdateDto userDto)
        {
            return Ok(userService.UpdateUser(User.Identity.Name, userDto));
        }

        [HttpPost("add-social-network")]
        public IActionResult AddSocialNetworks([FromBody] SocialNetworkDTO socialNetwork)
        {
            userService.AddSocialNetworks(User.Identity.Name, socialNetwork);
            return Ok();
        }
        
        [HttpDelete("remove-social-network")]
        public IActionResult RemoveSocialNetwork([FromBody] SocialNetworkDTO socialNetwork)
        {
            userService.RemoveSocialNetwork(User.Identity.Name, socialNetwork);
            return Ok();
        }
    }
}