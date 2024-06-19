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

        project.CreatedAt = DateTime.Now.ToUniversalTime();
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