using DataAccess.DTO.Education;
using DataAccess.Models.User;
using DataAccess.Repositories.Implementations;
using Microsoft.AspNetCore.Http;

namespace Services.Services;

public class EducationService(EducationRepository educationRepository)
{
    public async Task AddEducation(int userId, string username, IFormFile? file, EducationDTO education)
    {
        var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "images", username);
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        if (file != null)
        {
            var filePath = Path.Combine(directoryPath, file.FileName);
            await using var stream = File.Create(filePath);
            await file.CopyToAsync(stream);
            education.FilePath = filePath;
        }

        educationRepository.Add(new Education(education, userId));
    }

    public async Task UpdateEducation(int userId, IFormFile? file, EducationDTO education)
    {
        var educationFromDb = educationRepository.WithUser()
            .GetFirstOrDefault(e => e.UserId == userId && e.Id == education.Id);
        if (file != null)
        {
            if (File.Exists(educationFromDb.FilePath))
            {
                File.Delete(educationFromDb.FilePath);
            }

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "images", educationFromDb.User.Username,
                file.FileName);
            await using var stream = File.Create(filePath);
            await file.CopyToAsync(stream);
            education.FilePath = filePath;
        }
        else
        {
            education.FilePath = educationFromDb.FilePath;
        }

        educationRepository.Update(educationFromDb.Update(education));
    }

    public void RemoveEducation(int userId, int educationId)
    {
        var educationFromDb = educationRepository.GetFirstOrDefault(e => e.UserId == userId && e.Id == educationId);
        if (File.Exists(educationFromDb.FilePath))
        {
            File.Delete(educationFromDb.FilePath);
        }

        educationRepository.Remove(educationFromDb);
    }
}