using System.Security.Claims;
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
            var user = userRepository.GetUserWithSocialNetworks(user =>
                int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value) == user.Id);
            return Ok(new UserProfileDto(user));
        }

        [HttpPut("update")]
        public IActionResult UpdateUser(UserUpdateDTO userDto)
        {
            return Ok(userService.UpdateUser(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value), userDto));
        }

        [HttpPost("add-social-network")]
        public IActionResult AddSocialNetworks([FromBody] SocialNetworkDTO socialNetwork)
        {
            userService.AddSocialNetworks(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value), socialNetwork);
            return Ok();
        }

        [HttpDelete("remove-social-network")]
        public IActionResult RemoveSocialNetwork([FromBody] SocialNetworkDTO socialNetwork)
        {
            userService.RemoveSocialNetwork(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value), socialNetwork);
            return Ok();
        }
    }
}