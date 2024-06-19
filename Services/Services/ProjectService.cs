using DataAccess.DTO.Project;
using DataAccess.Models.Project;
using DataAccess.Repositories.Implementations;
using Microsoft.AspNetCore.Http;

namespace Services.Services;

public class ProjectService(ProjectRepository projectRepository, ProjectImageRepository projectImageRepository)
{
    public async Task AddProject(List<IFormFile> files, ProjectCreateDTO projectCreateDto, string username, int userId)
    {
        var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "images", username);
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        projectRepository.Add(new Project().Update(projectCreateDto));
        var project = projectRepository.GetFirstOrDefault(project => project.OwnerId == userId);

        foreach (var file in files)
        {
            var filePath = Path.Combine(directoryPath, file.FileName);
            await using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);
            projectImageRepository.Add(new ProjectImage() { ImagePath = filePath, ProjectId = project.Id });
        }

        projectImageRepository.SaveChanges();
    }
}