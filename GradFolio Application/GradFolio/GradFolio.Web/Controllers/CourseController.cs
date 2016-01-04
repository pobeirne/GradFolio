using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using GradFolio.Core.DTO;
using GradFolio.Core.Services;
using GradFolio.Core.ViewModels;
using Microsoft.AspNet.Identity;

namespace GradFolio.Web.Controllers
{
    [Authorize]
    public class CourseController : Controller
    {
        //public string UserId = "d5390018-e4ba-4ae4-add2-aaaa634f1a92";

        private readonly ILoggingService _loggingService;
        private readonly ICourseService _courseService;

        public CourseController(
            ICourseService courseService,
            ILoggingService loggingService)
        {
            _courseService = courseService;
            _loggingService = loggingService;
        }


        [HttpGet]
        public ActionResult ViewCourseList(string sort)
        {
            try
            {
                var courseList = GetCourseList(User.Identity.GetUserId());

                if (courseList != null)
                {
                    return PartialView("_ViewCourseList", courseList);
                }
            }
            catch (Exception ex)
            {
                _loggingService.Error("An error has occurred", ex);
                return Content("Item not found" + ex);
            }
            return Content("Item not found");
        }

        [HttpGet]
        public ActionResult ViewCourse(string itemId)
        {
            var course = GetCourse(User.Identity.GetUserId(), itemId);
            return PartialView("_ViewCourse", course);
        }


        [HttpGet]
        public ActionResult AddCourse()
        {
            return PartialView("_AddCourse");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCourse(CourseViewModel course)
        {
            if (ModelState.IsValid)
            {
                var isAdded = AddCourse(User.Identity.GetUserId(), course);
                if (isAdded)
                {
                    return Json(new {success = true});
                }
            }
            return PartialView("_AddCourse", course);
        }


        [HttpGet]
        public ActionResult EditCourse(string itemId)
        {
            var course = GetCourse(User.Identity.GetUserId(), itemId);
            return PartialView("_EditCourse", course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCourse(CourseViewModel course)
        {
            if (ModelState.IsValid)
            {
                var isUpdated = UpdateCourse(User.Identity.GetUserId(), course);
                if (isUpdated)
                {
                    return Json(new {success = true});
                }
            }
            return PartialView("_EditCourse", course);
        }


        [HttpGet]
        public ActionResult RemoveCourse(string itemId)
        {
            var course = GetCourse(User.Identity.GetUserId(), itemId);
            return PartialView("_RemoveCourse", course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("RemoveCourse")]
        public ActionResult RemoveCoursePost(string id)
        {
            {
                var isDeleted = _courseService.DeleteCourse(User.Identity.GetUserId(), id);
                if (isDeleted)
                {
                    return Json(new {success = true});
                }
            }
            var course = GetCourse(User.Identity.GetUserId(), id);
            ModelState.AddModelError(string.Empty, "The item cannot be removed");
            return PartialView("_RemoveCourse", course);
        }


        private IEnumerable<CourseViewModel> GetCourseList(string userId)
        {
            var courses = _courseService.GetAllCourseByUserId(userId);
            if (courses == null) return null;


            var result = courses.Select(course => new CourseViewModel()
            {
                Id = course.Id,
                Title = course.Title,
                College = course.College,
                Summary = course.Summary,
                Location = course.Location,
                StartDate = course.StartDate,
                EndDate = course.EndDate,
                IsCurrent = course.IsCurrent
            }).ToList();

            return result;
        }

        private CourseViewModel GetCourse(string userId, string courseId)
        {
            var course = _courseService.GetCourseById(userId, courseId);
            if (course == null) return null;


            var result = new CourseViewModel()
            {
                Id = course.Id,
                Title = course.Title,
                College = course.College,
                Summary = course.Summary,
                Location = course.Location,
                StartDate = course.StartDate,
                EndDate = course.EndDate,
                IsCurrent = course.IsCurrent
            };

            return result;
        }

        private bool AddCourse(string userId, CourseViewModel course)
        {
            var addRecord = new CourseDto()
            {
                UserId = userId,
                Title = course.Title,
                College = course.College,
                Summary = course.Summary,
                Location = course.Location,
                StartDate = course.StartDate,
                EndDate = course.EndDate,
                IsCurrent = course.IsCurrent
            };
            return _courseService.InsertCourse(userId, addRecord);
        }

        private bool UpdateCourse(string userId, CourseViewModel course)
        {
            var updateRecord = new CourseDto()
            {
                Id = course.Id,
                UserId = userId,
                Title = course.Title,
                College = course.College,
                Summary = course.Summary,
                Location = course.Location,
                StartDate = course.StartDate,
                EndDate = course.EndDate,
                IsCurrent = course.IsCurrent
            };
            return _courseService.UpdateCourse(userId, updateRecord);
        }
    }
}