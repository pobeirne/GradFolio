using System.Collections.Generic;
using GradFolio.Core.DTO;

namespace GradFolio.Core.Services
{
    public interface IExperienceService
    {
        ExperienceDto GetExperienceById(string userId, string experienceId);
        IEnumerable<ExperienceDto> GetAllExperienceByUserId(string userId);
        bool InsertExperience(string userId, ExperienceDto experience);
        bool UpdateExperience(string userId, ExperienceDto experience);
        bool DeleteExperience(string userId, string experienceId);
    }
}
