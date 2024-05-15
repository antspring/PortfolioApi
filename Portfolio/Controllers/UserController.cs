using DataAccess;
using DataAccess.DTO.User;
using DataAccess.Repositories.Implementations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Portfolio.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(UserRepository userRepository) : ControllerBase
    {
        [HttpGet("get")]
        public IActionResult GetUser()
        {
            var user = userRepository.GetFirstOrDefault(user => User.Identity.Name == user.Username);
            return Ok(new UserProfileDto(user));
        }

        [HttpPatch("update")]
        public IActionResult UpdateUser(UserUpdateDto userDto)
        {
            var currentUser = userRepository.GetFirstOrDefault(user => User.Identity.Name == user.Username);
            userRepository.Update(currentUser.Update(userDto));
            return Ok(new UserProfileDto(currentUser));
        }
    }
}