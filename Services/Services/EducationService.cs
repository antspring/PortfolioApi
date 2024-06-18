using DataAccess.DTO.Education;
using DataAccess.Models.User;
using DataAccess.Repositories.Implementations;

namespace Services.Services;

public class EducationService(EducationRepository educationRepository, UserRepository userRepository)
{
    public void AddEducation(int userId, EducationDTO education)
    {
        educationRepository.Add(new Education(education, userId));
    }

    public void UpdateEducation(int userId, EducationDTO education)
    {
        var educationFromDb = educationRepository.GetFirstOrDefault(e => e.UserId == userId && e.Id == education.Id);
        educationRepository.Update(educationFromDb.Update(education));
    }
}