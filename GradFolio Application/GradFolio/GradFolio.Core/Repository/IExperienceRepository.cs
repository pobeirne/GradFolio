using System.Collections.Generic;
using GradFolio.Core.DTO;

namespace GradFolio.Core.Repository
{
    public interface IExperienceRepository
    {
        ExperienceDto GetExperienceById(string experienceId);
        IEnumerable<ExperienceDto> GetAllExperienceByUserId(string userId);
        bool InsertExperience(ExperienceDto experience);
        bool UpdateExperience(ExperienceDto experience);
        bool DeleteExperience(string experienceId);
        bool Save();
    }
}
