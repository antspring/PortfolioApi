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
            [FromForm] ProjectDTO projectDto)
        {
            if (projectDto.IsTeam)
            {
                try
                {
                    await projectService.AddTeamProject(files, projectDto,
                        int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value));
                }
                catch (Exception e)
                {
                    return NotFound(e.Message);
                }
            }
            else
            {
                await projectService.AddProject(files, projectDto, User.Identity.Name,
                    int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value));
            }

            return Ok();
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateProject([FromForm] List<IFormFile> files,
            [FromForm] ProjectDTO projectDto)
        {
            await projectService.UpdateProject(files, projectDto, User.Identity.Name,
                int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value));
            return Ok();
        }

        [HttpDelete("remove")]
        public IActionResult RemoveProject(ProjectRemoveDTO projectRemoveDto)
        {
            try
            {
                projectService.RemoveProject(projectRemoveDto);
                return Ok();
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
        
        [AllowAnonymous]
        [HttpGet("get-all")]
        public IActionResult GetAllProjects()
        {
            return Ok(projectService.GetAllProjects());
        }
        
        [AllowAnonymous]
        [HttpGet("get/{id:int}")]
        public IActionResult GetProject(int id)
        {
            return Ok(projectService.GetProject(id));
        }
    }
}