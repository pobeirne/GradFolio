using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using GradFolio.Core.DTO;
using GradFolio.Core.Model;
using GradFolio.Core.Repository;
using GradFolio.Infrastructure.Data;

namespace GradFolio.Infrastructure.Repository
{
    public class SkillRepository : ISkillRepository
    {
        private readonly GradFolioContext _context = new GradFolioContext();

        public SkillDto GetSkillById(string skillId)
        {
            var skill = _context.Skills.FirstOrDefault(x => x.Id.ToString() == skillId);
            if (skill == null) return null;
            return new SkillDto
            {
                Id = skill.Id,
                UserId = skill.UserId,
                Title = skill.Title,
                Level = skill.Level,
                Summary = skill.Summary,
                CreateDate = skill.CreateDate
            };
        }

        public IEnumerable<SkillDto> GetAllSkillByUserId(string userId)
        {
            var skills = _context.Skills.Where(x => x.UserId == userId);
            if (!skills.Any()) return null;

            var resultList = new List<SkillDto>();
            foreach (var skill in skills)
            {
                resultList.Add(new SkillDto
                {
                    Id = skill.Id,
                    UserId = skill.UserId,
                    Title = skill.Title,
                    Level = skill.Level,
                    Summary = skill.Summary,
                    CreateDate = skill.CreateDate
                });
            }
            return resultList;
        }

        public bool InsertSkill(SkillDto skill)
        {
            try
            {
                var isInserted = _context.Skills.Add(new Skill
                {
                    UserId = skill.UserId,
                    Title = skill.Title,
                    Level = skill.Level,
                    Summary = skill.Summary,
                    CreateDate = DateTime.Now
                });
                if (isInserted != null)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return false;
        }

        public bool UpdateSkill(SkillDto skill)
        {
            var record = _context.Skills.FirstOrDefault(x => x.Id == skill.Id);
            if (record == null) return false;

            record.Title = skill.Title;
            record.Level = skill.Level;
            record.Summary = skill.Summary;

            _context.Skills.AddOrUpdate(record);
            return _context.SaveChanges() == 1;
        }

        public bool DeleteSkill(string skillId)
        {
            var record = _context.Skills.FirstOrDefault(x => x.Id.ToString() == skillId);
            return _context.Skills.Remove(record) != null;
        }

        public bool Save()
        {
            try
            {
                return _context.SaveChanges() == 1;
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
    }
}
