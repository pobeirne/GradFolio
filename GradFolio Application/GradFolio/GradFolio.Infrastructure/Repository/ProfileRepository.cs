using System;
using System.Data.Entity.Migrations;
using System.Linq;
using GradFolio.Core.DTO;
using GradFolio.Core.Model;
using GradFolio.Core.Repository;
using GradFolio.Infrastructure.Data;

namespace GradFolio.Infrastructure.Repository
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly GradFolioContext _context = new GradFolioContext();

        public ProfileDto GetUserProfileByUserId(string userId)
        {
            var profile = _context.Profiles.FirstOrDefault(x => x.UserId == userId);

            if (profile == null) return null;

            var result = new ProfileDto
            {
                Id = profile.Id,
                UserId = profile.UserId,
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                Title = profile.Title,
                Summary = profile.Summary,
                Location = profile.Location,
                Mobile = profile.Mobile,
                Phone = profile.Phone,
                IsAvailable = profile.IsAvailable,
                AvailableFromDate = profile.AvailableFromDate,
                ImageUrl = profile.ImageUrl,
                PortfolioUrl = profile.PortfolioUrl,
                LinkedInUrl = profile.LinkedInUrl,
                JoinDate = profile.CreateDate
            };
            return result;
        }

        public bool InsertProfile(ProfileDto profile)
        {
            return _context.Profiles.Add(new Profile
            {
                UserId = profile.UserId,
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                Title = profile.Title,
                Summary = profile.Summary,
                Location = profile.Location,
                Mobile = profile.Mobile,
                Phone = profile.Phone,
                IsAvailable = profile.IsAvailable,
                AvailableFromDate = profile.AvailableFromDate,
                ImageUrl = profile.ImageUrl,
                PortfolioUrl = profile.PortfolioUrl,
                LinkedInUrl = profile.LinkedInUrl,
                CreateDate = DateTime.Now
            }) != null;
        }

        public bool UpdateProfile(ProfileDto profile)
        {
            var record = _context.Profiles.FirstOrDefault(x => x.UserId == profile.UserId);

            if (record == null) return false;

            record.FirstName = profile.FirstName;
            record.LastName = profile.LastName;
            record.Title = profile.Title;
            record.Summary = profile.Summary;
            record.Location = profile.Location;
            record.Mobile = profile.Mobile;
            record.Phone = profile.Phone;
            record.IsAvailable = profile.IsAvailable;
            record.AvailableFromDate = profile.AvailableFromDate;
            record.ImageUrl = profile.ImageUrl;
            record.PortfolioUrl = profile.PortfolioUrl;
            record.LinkedInUrl = profile.LinkedInUrl;

            _context.Profiles.AddOrUpdate(record);
            return _context.SaveChanges() == 1;
        }

        public bool DeleteProfile(string userId)
        {
            var record = _context.Profiles.FirstOrDefault(x => x.UserId == userId);
            return _context.Profiles.Remove(record) != null;
        }

        public bool Save()
        {
            return _context.SaveChanges() == 1;
        }
    }
}
