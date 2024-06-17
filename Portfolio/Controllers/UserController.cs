using DataAccess;
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
            var user = userRepository.GetFirstOrDefault(user => User.Identity.Name == user.Username);
            return Ok(new UserProfileDto(user));
        }

        [HttpPatch("update")]
        public IActionResult UpdateUser(UserUpdateDto userDto)
        {
            return Ok(userService.UpdateUser(User.Identity.Name, userDto));
        }
    }
}