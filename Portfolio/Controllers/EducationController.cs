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
        public IActionResult AddEducation(EducationDTO education)
        {
            educationService.AddEducation(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value), education);
            return Ok();
        }
    }
}