using System.Security.Claims;
using DataAccess.DTO.Project;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Services;

namespace Portfolio.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController(ProjectService projectService) : ControllerBase
    {
        [HttpPost("add")]
        public async Task<IActionResult> AddProject([FromForm] List<IFormFile> files,
            [FromForm] ProjectCreateDTO projectCreateDto)
        {
            await projectService.AddProject(files, projectCreateDto, User.Identity.Name,
                int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value));
            return Ok();
        }
    }
}