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
    public class ExperienceRepository : IExperienceRepository
    {
        private readonly GradFolioContext _context = new GradFolioContext();

        public ExperienceDto GetExperienceById(string experienceId)
        {
            var experience = _context.Experiences.FirstOrDefault(x => x.Id.ToString() == experienceId);
            if (experience == null) return null;

            return new ExperienceDto
            {
                Id = experience.Id,
                UserId = experience.UserId,
                Title = experience.Title,
                Company = experience.Company,
                Summary = experience.Summary,
                Location = experience.Location,
                StartDate = experience.StartDate,
                EndDate = experience.EndDate,
                IsCurrent = experience.IsCurrent,
                CreateDate = experience.CreateDate
            };
        }

        public IEnumerable<ExperienceDto> GetAllExperienceByUserId(string userId)
        {
            var experiences = _context.Experiences.Where(x => x.UserId == userId);
            if (!experiences.Any()) return null;


            var resultList = new List<ExperienceDto>();
            foreach (var experience in experiences)
            {
                resultList.Add(new ExperienceDto
                {
                    Id = experience.Id,
                    UserId = experience.UserId,
                    Title = experience.Title,
                    Company = experience.Company,
                    Summary = experience.Summary,
                    Location = experience.Location,
                    StartDate = experience.StartDate,
                    EndDate = experience.EndDate,
                    IsCurrent = experience.IsCurrent,
                    CreateDate = experience.CreateDate
                });
            }
            return resultList;
        }

        public bool InsertExperience(ExperienceDto experience)
        {
            return _context.Experiences.Add(new Experience
            {
                UserId = experience.UserId,
                Title = experience.Title,
                Company = experience.Company,
                Summary = experience.Summary,
                Location = experience.Location,
                StartDate = experience.StartDate,
                EndDate = experience.EndDate,
                IsCurrent = experience.IsCurrent,
                CreateDate = DateTime.Now
            }) != null;
        }

        public bool UpdateExperience(ExperienceDto experience)
        {
            var record = _context.Experiences.FirstOrDefault(x => x.Id == experience.Id);
            if (record == null) return false;

            record.Title = experience.Title;
            record.Company = experience.Company;
            record.Summary = experience.Summary;
            record.Location = experience.Location;
            record.StartDate = experience.StartDate;
            record.EndDate = experience.EndDate;
            record.IsCurrent = experience.IsCurrent;

            _context.Experiences.AddOrUpdate(record);
            return _context.SaveChanges() == 1;
        }

        public bool DeleteExperience(string experienceId)
        {
            var record = _context.Experiences.FirstOrDefault(x => x.Id.ToString() == experienceId);
            return _context.Experiences.Remove(record) != null;
        }

        public bool Save()
        {
            return _context.SaveChanges() == 1;
        }
    }
}
