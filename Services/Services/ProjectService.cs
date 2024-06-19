using DataAccess.DTO.Project;
using DataAccess.Models.Project;
using DataAccess.Repositories.Implementations;
using Microsoft.AspNetCore.Http;

namespace Services.Services;

public class ProjectService(ProjectRepository projectRepository, ProjectImageRepository projectImageRepository)
{
    public async Task AddProject(List<IFormFile> files, ProjectDTO projectDto, string username, int userId)
    {
        projectRepository.Add(new Project().Update(projectDto));
        Project project;
        if (projectDto.IsTeam)
        {
            project = projectRepository.GetFirstOrDefault(project => project.OwnerTeamId == userId);
        }
        else
        {
            project = projectRepository.GetFirstOrDefault(project => project.OwnerId == userId);
        }

        await SavingFiles(files, username, project.Id);
        projectImageRepository.SaveChanges();
    }

    public async Task UpdateProject(List<IFormFile> files, ProjectDTO projectDto, string username,
        int userId)
    {
        Project project;
        if (projectDto.IsTeam)
        {
            project = projectRepository.GetFirstOrDefault(project => project.OwnerTeamId == userId);
        }
        else
        {
            project = projectRepository.GetFirstOrDefault(project => project.OwnerId == userId);
        }

        projectRepository.Update(project.Update(projectDto));
        await SavingFiles(files, username, project.Id);
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
}