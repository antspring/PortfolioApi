using System.Security.Claims;
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
            teamService.AddTeam(teamDto, int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value));
            return Ok();
        }

        [HttpGet("get/{id:int}")]
        public IActionResult GetTeam(int teamId)
        {
            return Ok(teamService.GetTeam(teamId, int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)));
        }

        [HttpPost("add-user")]
        public IActionResult AddUserToTeam(int teamId, string username)
        {
            teamService.AddUserToTeam(teamId, username);
            return Ok();
        }

        [HttpDelete("remove/{id:int}")]
        public IActionResult RemoveTeam(int id)
        {
            teamService.RemoveTeam(id);
            return Ok();
        }
    }
}