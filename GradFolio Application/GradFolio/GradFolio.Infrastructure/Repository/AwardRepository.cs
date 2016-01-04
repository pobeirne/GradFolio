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
    public class AwardRepository : IAwardRepository
    {
        private readonly GradFolioContext _context = new GradFolioContext();

        public AwardDto GetAwardById(string awardId)
        {
            var award = _context.Awards.FirstOrDefault(x => x.Id.ToString() == awardId);
            if (award == null) return null;
            return new AwardDto
            {
                Id = award.Id,
                UserId = award.UserId,
                Title = award.Title,
                Level = award.Level,
                IssuedBy = award.IssuedBy,
                IssuedDate = award.IssuedDate,
                CreateDate = award.CreateDate
            };
        }

        public IEnumerable<AwardDto> GetAllAwardByUserId(string userId)
        {
            var awards = _context.Awards.Where(x => x.UserId == userId);
            if (!awards.Any()) return null;

            var resultList = new List<AwardDto>();
            foreach (var award in awards)
            {
                resultList.Add(new AwardDto
                {
                    Id = award.Id,
                    UserId = award.UserId,
                    Title = award.Title,
                    Level = award.Level,
                    IssuedBy = award.IssuedBy,
                    IssuedDate = award.IssuedDate,
                    CreateDate = award.CreateDate
                });
            }
            return resultList;
        }

        public bool InsertAward(AwardDto award)
        {
            return _context.Awards.Add(new Award
            {
                UserId = award.UserId,
                Title = award.Title,
                Level = award.Level,
                IssuedBy = award.IssuedBy,
                IssuedDate = award.IssuedDate,
                CreateDate = DateTime.Now
            }) != null;
        }

        public bool UpdateAward(AwardDto award)
        {
            var record = _context.Awards.FirstOrDefault(x => x.Id == award.Id);
            if (record == null) return false;

            record.Title = award.Title;
            record.Level = award.Level;
            record.IssuedBy = award.IssuedBy;
            record.IssuedDate = award.IssuedDate;

            _context.Awards.AddOrUpdate(record);
            return _context.SaveChanges() == 1;
        }

        public bool DeleteAward(string awardId)
        {
            var record = _context.Awards.FirstOrDefault(x => x.Id.ToString() == awardId);
            return _context.Awards.Remove(record) != null;
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
