using System.Collections.Generic;
using GradFolio.Core.DTO;

namespace GradFolio.Core.Services
{
    public interface ICourseService
    {
        CourseDto GetCourseById(string userId, string courseId);
        IEnumerable<CourseDto> GetAllCourseByUserId(string userId);
        bool InsertCourse(string userId, CourseDto course);
        bool UpdateCourse(string userId, CourseDto course);
        bool DeleteCourse(string userId, string courseId);
    }
}
