using DataAccess.DTO.Project;
using DataAccess.Models.Project;
using DataAccess.Repositories.Implementations;
using Microsoft.AspNetCore.Http;

namespace Services.Services;

public class ProjectService(
    ProjectRepository projectRepository,
    ProjectImageRepository projectImageRepository,
    TeamRepository teamRepository)
{
    public async Task AddProject(List<IFormFile> files, ProjectDTO projectDto, string ownerName, int ownerId)
    {
        projectRepository.Add(new Project().Update(projectDto));
        Project project;
        if (projectDto.IsTeam)
        {
            project = projectRepository.GetFirstOrDefault(project => project.OwnerTeamId == ownerId);
        }
        else
        {
            project = projectRepository.GetFirstOrDefault(project => project.OwnerId == ownerId);
        }

        project.CreatedAt = DateTime.Now.ToUniversalTime();
        projectRepository.Update(project);
        await SavingFiles(files, ownerName, project.Id);
        projectImageRepository.SaveChanges();
    }

    public async Task UpdateProject(List<IFormFile> files, ProjectDTO projectDto, string ownerName,
        int ownerId)
    {
        Project project;
        if (projectDto.IsTeam)
        {
            project = projectRepository.GetFirstOrDefault(project => project.OwnerTeamId == ownerId);
        }
        else
        {
            project = projectRepository.GetFirstOrDefault(project => project.OwnerId == ownerId);
        }

        projectRepository.Update(project.Update(projectDto));
        await SavingFiles(files, ownerName, project.Id);
        projectImageRepository.SaveChanges();
    }

    private async Task SavingFiles(List<IFormFile> files, string username, int projectId)
    {
        var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "images", username);
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        foreach (var file in files)
        {
            var filePath = Path.Combine(directoryPath, file.FileName);
            await using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);
            projectImageRepository.Add(new ProjectImage() { ImagePath = filePath, ProjectId = projectId });
        }
    }

    public void RemoveProject(ProjectRemoveDTO projectRemoveDto)
    {
        Project project;
        if (projectRemoveDto.IsTeam)
        {
            project = projectRepository.WithImages()
                .GetFirstOrDefault(project =>
                    project.Id == projectRemoveDto.Id && project.OwnerTeamId == projectRemoveDto.OwnerId);
        }
        else
        {
            project = projectRepository.WithImages()
                .GetFirstOrDefault(project =>
                    project.Id == projectRemoveDto.Id && project.OwnerId == projectRemoveDto.OwnerId);
        }

        if (project == default)
        {
            throw new Exception("Project not found.");
        }

        foreach (var file in project.Images)
        {
            File.Delete(file.ImagePath);
        }

        projectImageRepository.ExecuteRemove(projectRemoveDto.Id);
        projectRepository.Remove(project);
    }

    public async Task AddTeamProject(List<IFormFile> files, ProjectDTO projectDto, int userId)
    {
        var team = teamRepository.WithUsers().GetFirstOrDefault(team =>
            team.Id == projectDto.OwnerId && team.Users.Any(user => user.Id == userId));
        if (team == default)
        {
            throw new Exception("Team not found.");
        }

        await AddProject(files, projectDto, team.Name, team.Id);
    }

    public List<ProjectViewDTO> GetAllProjects()
    {
        return projectRepository.WithOwner().WithOwnerTeam().WithImages().GetAll()
            .Select(project => new ProjectViewDTO(project)).ToList();
    }
    
    public ProjectViewDTO GetProject(int id)
    {
        return new ProjectViewDTO(projectRepository.WithOwner().WithOwnerTeam().WithImages()
            .GetFirstOrDefault(project => project.Id == id));
    }
}