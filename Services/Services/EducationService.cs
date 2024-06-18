using DataAccess.DTO.Education;
using DataAccess.Models.User;
using DataAccess.Repositories.Implementations;

namespace Services.Services;

public class EducationService(EducationRepository educationRepository)
{
    public void AddEducation(int userId, EducationDTO education)
    {
        educationRepository.Add(new Education(education, userId));
    }
}