using DataAccess.DTO.Team;
using DataAccess.Models.Project;
using DataAccess.Repositories.Implementations;

namespace Services.Services;

public class TeamService(TeamRepository teamRepository, UserRepository userRepository)
{
    public void AddTeam(TeamDTO teamDto, int userId)
    {
        var user = userRepository.GetFirstOrDefault(user => user.Id == userId);
        var team = new Team() { Name = teamDto.Name };
        team.Users.Add(user);
        teamRepository.Add(team);
    }

    public Team GetTeam(int teamId, int userId)
    {
        var user = userRepository.WithTeams().GetFirstOrDefault(user => user.Id == userId);
        var team = user.Teams.FirstOrDefault(team => team.Id == teamId);
        if (team == default)
        {
            throw new Exception("Team not found.");
        }
        
        return team;
    }

    public void AddUserToTeam(int teamId, string username)
    {
        var team = teamRepository.GetFirstOrDefault(team => team.Id == teamId);
        var user = userRepository.GetFirstOrDefault(user => user.Username == username);
        teamRepository.AddUserToTeam(team, user);
    }

    public void RemoveTeam(int id)
    {
        var team = teamRepository.GetFirstOrDefault(team => team.Id == id);
        teamRepository.Remove(team);
    }
}