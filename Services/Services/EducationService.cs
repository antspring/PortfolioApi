using DataAccess.DTO.Education;
using DataAccess.Models.User;
using DataAccess.Repositories.Implementations;
using Microsoft.AspNetCore.Http;

namespace Services.Services;

public class EducationService(EducationRepository educationRepository)
{
    public async Task AddEducation(int userId, string username, IFormFile file, EducationDTO education)
    {
        var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "images", username);
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        var filePath = Path.Combine(directoryPath, file.FileName);
        await using var stream = File.Create(filePath);
        await file.CopyToAsync(stream);
        education.FilePath = filePath;
        educationRepository.Add(new Education(education, userId));
    }

    public void UpdateEducation(int userId, EducationDTO education)
    {
        var educationFromDb = educationRepository.GetFirstOrDefault(e => e.UserId == userId && e.Id == education.Id);
        educationRepository.Update(educationFromDb.Update(education));
    }
}