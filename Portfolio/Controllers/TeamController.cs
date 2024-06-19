using DataAccess.DTO.Team;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Services;

namespace Portfolio.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController(TeamService teamService) : ControllerBase
    {
        [HttpPost("add")]
        public IActionResult AddTeam(TeamDTO teamDto)
        {
            teamService.AddTeam(teamDto);
            return Ok();
        }
    }
}