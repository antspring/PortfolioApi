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
        public async Task<IActionResult> AddEducation([FromForm] IFormFile file, [FromForm] EducationDTO education)
        {
            await educationService.AddEducation(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value),
                User.Identity.Name, file, education);
            return Ok();
        }

        [HttpPut("update")]
        public IActionResult UpdateEducation(EducationDTO education)
        {
            educationService.UpdateEducation(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value), education);
            return Ok();
        }
    }
}