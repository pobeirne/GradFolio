using System.Collections.Generic;
using GradFolio.Core.DTO;
using GradFolio.Core.ViewModels;

namespace GradFolio.Core.Services
{
    public interface ISkillService
    {
        SkillDto GetSkillById(string userId, string skillId);
        IEnumerable<SkillDto> GetAllSkillByUserId(string userId);
        bool InsertSkill(string userId, SkillDto skill);
        bool UpdateSkill(string userId, SkillDto skill);
        bool DeleteSkill(string userId, string skillId);
    }
}
