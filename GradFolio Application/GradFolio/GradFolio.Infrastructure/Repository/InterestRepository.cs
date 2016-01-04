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
    public class InterestRepository : IInterestRepository
    {
        private readonly GradFolioContext _context = new GradFolioContext();

        public InterestDto GetInterestById(string interestId)
        {
            var interest = _context.Interests.FirstOrDefault(x => x.Id.ToString() == interestId);
            if (interest == null) return null;
            return new InterestDto
            {
                Id = interest.Id,
                UserId = interest.UserId,
                Title = interest.Title,
                Summary = interest.Summary,
                CreateDate = interest.CreateDate
            };
        }

        public IEnumerable<InterestDto> GetAllInterestByUserId(string userId)
        {
            var interests = _context.Interests.Where(x => x.UserId == userId);
            if (!interests.Any()) return null;

            var resultList = new List<InterestDto>();
            foreach (var interest in interests)
            {
                resultList.Add(new InterestDto
                {
                    Id = interest.Id,
                    UserId = interest.UserId,
                    Title = interest.Title,
                    Summary = interest.Summary,
                    CreateDate = interest.CreateDate
                });
            }
            return resultList;
        }

        public bool InsertInterest(InterestDto interest)
        {
            return _context.Interests.Add(new Interest
            {
                UserId = interest.UserId,
                Title = interest.Title,
                Summary = interest.Summary,
                CreateDate = DateTime.Now
            }) != null;
        }

        public bool UpdateInterest(InterestDto interest)
        {
            var record = _context.Interests.FirstOrDefault(x => x.Id == interest.Id);
            if (record == null) return false;

            record.Title = interest.Title;
            record.Summary = interest.Summary;

            _context.Interests.AddOrUpdate(record);
            return _context.SaveChanges() == 1;
        }

        public bool DeleteInterest(string interestId)
        {
            var record = _context.Interests.FirstOrDefault(x => x.Id.ToString() == interestId);
            return _context.Interests.Remove(record) != null;
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
