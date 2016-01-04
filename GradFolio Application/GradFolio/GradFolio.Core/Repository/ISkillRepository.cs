using System.Collections.Generic;
using GradFolio.Core.DTO;

namespace GradFolio.Core.Repository
{
    public interface ISkillRepository
    {
        SkillDto GetSkillById(string skillId);
        IEnumerable<SkillDto> GetAllSkillByUserId(string userId);
        bool InsertSkill(SkillDto skill);
        bool UpdateSkill(SkillDto skill);
        bool DeleteSkill(string skillId);
        bool Save();
    }
}
