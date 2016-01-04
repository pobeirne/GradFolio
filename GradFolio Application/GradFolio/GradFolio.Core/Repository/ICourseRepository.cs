using System.Collections.Generic;
using GradFolio.Core.DTO;

namespace GradFolio.Core.Repository
{
    public interface ICourseRepository
    {
        CourseDto GetCourseById(string courseId);
        IEnumerable<CourseDto> GetAllCourseByUserId(string userId);
        bool InsertCourse(CourseDto course);
        bool UpdateCourse(CourseDto course);
        bool DeleteCourse(string courseId);
        bool Save();
    }
}
