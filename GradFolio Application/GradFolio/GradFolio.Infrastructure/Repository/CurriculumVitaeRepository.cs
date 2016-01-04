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
    public class CurriculumVitaeRepository : ICurriculumVitaeRepository
    {
        private readonly GradFolioContext _context = new GradFolioContext();

        public CurriculumVitaeDto GetCurriculumVitaeById(string cvId)
        {
            var cv = _context.CurriculumVitaes.FirstOrDefault(x => x.Id.ToString() == cvId);
            if (cv == null) return null;
            return new CurriculumVitaeDto
            {
                Id = cv.Id,
                UserId = cv.UserId,
                Name = cv.Name,
                Type = cv.Type,
                RefNum = cv.RefNum,
                Experience1 = cv.Experience1,
                Experience2 = cv.Experience2,
                Course1 = cv.Course1,
                Course2 = cv.Course2,
                CreateDate = cv.CreateDate
            };
        }

        public IEnumerable<CurriculumVitaeDto> GetAllCurriculumVitaeByUserId(string userId)
        {
            var cvs = _context.CurriculumVitaes.Where(x => x.UserId == userId);
            if (!cvs.Any()) return null;


            var resultList = new List<CurriculumVitaeDto>();
            foreach (var cv in cvs)
            {
                resultList.Add(new CurriculumVitaeDto
                {
                    Id = cv.Id,
                    UserId = cv.UserId,
                    Name = cv.Name,
                    Type = cv.Type,
                    RefNum = cv.RefNum,
                    Experience1 = cv.Experience1,
                    Experience2 = cv.Experience2,
                    Course1 = cv.Course1,
                    Course2 = cv.Course2,
                    CreateDate = cv.CreateDate
                });
            }
            return resultList;
        }

        public bool InsertCurriculumVitae(CurriculumVitaeDto cv)
        {
            return _context.CurriculumVitaes.Add(new CurriculumVitae
            {
                UserId = cv.UserId,
                Name = cv.Name,
                Type = cv.Type,
                Experience1 = cv.Experience1,
                Experience2 = cv.Experience2,
                Course1 = cv.Course1,
                Course2 = cv.Course2,
                CreateDate = DateTime.Now
            }) != null;
        }

        public bool UpdateCurriculumVitae(CurriculumVitaeDto cv)
        {
            var record = _context.CurriculumVitaes.FirstOrDefault(x => x.Id == cv.Id);
            if (record == null) return false;
            
            record.Name = cv.Name;
            record.Experience1 = cv.Experience1;
            record.Experience2 = cv.Experience2;
            record.Course1 = cv.Course1;
            record.Course2 = cv.Course2;

            _context.CurriculumVitaes.AddOrUpdate(record);
            return _context.SaveChanges() == 1;
        }

        public bool DeleteCurriculumVitae(string cvId)
        {
            var record = _context.CurriculumVitaes.FirstOrDefault(x => x.Id.ToString() == cvId);
            return _context.CurriculumVitaes.Remove(record) != null;
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
