using System.Security.Claims;
using DataAccess.DTO.Education;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Services;

namespace Portfolio.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EducationController(EducationService educationService) : ControllerBase
    {
        [HttpPost("add")]
        public async Task<IActionResult> AddEducation([FromForm] IFormFile? file, [FromForm] EducationDTO education)
        {
            await educationService.AddEducation(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value),
                User.Identity.Name, file, education);
            return Ok();
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateEducation([FromForm] IFormFile? file, [FromForm] EducationDTO education)
        {
            await educationService.UpdateEducation(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value), file,
                education);
            return Ok();
        }

        [HttpDelete("remove/{educationId:int}")]
        public IActionResult RemoveEducation(int educationId)
        {
            educationService.RemoveEducation(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value),
                educationId);
            return Ok(educationId);
        }

        [AllowAnonymous]
        [HttpGet("get/{username}/{educationId:int}")]
        public IActionResult GetEducation(string username, int educationId)
        {
            var education = educationService.GetEducation(username, educationId);
            return Ok(education);
        }
        
        [AllowAnonymous]
        [HttpGet("get-all/{username}")]
        public IActionResult GetAllEducations(string username)
        {
            var educations = educationService.GetAllEducations(username);
            return Ok(educations);
        }
    }
}