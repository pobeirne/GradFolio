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
    public class CourseRepository : ICourseRepository
    {
        private readonly GradFolioContext _context = new GradFolioContext();

        public CourseDto GetCourseById(string courseId)
        {
            var course = _context.Courses.FirstOrDefault(x => x.Id.ToString() == courseId);
            if (course == null) return null;
            return new CourseDto
            {
                Id = course.Id,
                UserId = course.UserId,
                Title = course.Title,
                College = course.College,
                Summary = course.Summary,
                Location = course.Location,
                StartDate = course.StartDate,
                EndDate = course.EndDate,
                IsCurrent = course.IsCurrent,
                CreateDate = course.CreateDate
            };
        }

        public IEnumerable<CourseDto> GetAllCourseByUserId(string userId)
        {
            var courses = _context.Courses.Where(x => x.UserId == userId);
            if (!courses.Any()) return null;


            var resultList = new List<CourseDto>();
            foreach (var course in courses)
            {
                resultList.Add(new CourseDto
                {
                    Id = course.Id,
                    UserId = course.UserId,
                    Title = course.Title,
                    College = course.College,
                    Summary = course.Summary,
                    Location = course.Location,
                    StartDate = course.StartDate,
                    EndDate = course.EndDate,
                    IsCurrent = course.IsCurrent,
                    CreateDate = course.CreateDate
                });
            }
            return resultList;
        }

        public bool InsertCourse(CourseDto course)
        {
            return _context.Courses.Add(new Course
            {
                UserId = course.UserId,
                Title = course.Title,
                College = course.College,
                Summary = course.Summary,
                Location = course.Location,
                StartDate = course.StartDate,
                EndDate = course.EndDate,
                IsCurrent = course.IsCurrent,
                CreateDate = DateTime.Now
            }) != null;
        }

        public bool UpdateCourse(CourseDto course)
        {
            var record = _context.Courses.FirstOrDefault(x => x.Id == course.Id);
            if (record == null) return false;

            record.Title = course.Title;
            record.College = course.College;
            record.Summary = course.Summary;
            record.Location = course.Location;
            record.StartDate = course.StartDate;
            record.EndDate = course.EndDate;
            record.IsCurrent = course.IsCurrent;

            _context.Courses.AddOrUpdate(record);
            return _context.SaveChanges() == 1;
        }

        public bool DeleteCourse(string courseId)
        {
            var record = _context.Courses.FirstOrDefault(x => x.Id.ToString() == courseId);
            return _context.Courses.Remove(record) != null;
        }

        public bool Save()
        {
            return _context.SaveChanges() == 1;
        }
    }
}
