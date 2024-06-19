using DataAccess.DTO.Team;
using DataAccess.Models.Project;
using DataAccess.Repositories.Implementations;

namespace Services.Services;

public class TeamService(TeamRepository teamRepository)
{
    public void AddTeam(TeamDTO teamDto)
    {
        teamRepository.Add(new Team() { Name = teamDto.Name });
    }

    public void RemoveTeam(int id)
    {
        var team = teamRepository.GetFirstOrDefault(team => team.Id == id);
        teamRepository.Remove(team);
    }
}