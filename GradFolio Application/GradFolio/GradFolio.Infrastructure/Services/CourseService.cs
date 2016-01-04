using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GradFolio.Core.DTO;
using GradFolio.Core.Repository;
using GradFolio.Core.Services;
using GradFolio.Infrastructure.Services.Helpers;

namespace GradFolio.Infrastructure.Services
{
    public class CourseService : DataAnnotationsValidator, ICourseService
    {
        private readonly ILoggingService _loggingService;
        private readonly ICourseRepository _courseRepository;
        private readonly IUserRepository _userRepository;

        public CourseService(ILoggingService loggingService,
            ICourseRepository courseRepository,
            IUserRepository userRepository)
        {
            _loggingService = loggingService;
            _courseRepository = courseRepository;
            _userRepository = userRepository;
        }

        public CourseDto GetCourseById(string userId, string courseId)
        {
            try
            {
                //Validate user
                if (_userRepository.IsAuthenticated(userId))
                {
                    //GetUserProfile
                    var course = _courseRepository.GetCourseById(courseId);
                    if (course != null)
                    {
                        //Success
                        return course;
                    }
                    _loggingService.Info("Not profile for the user found: " + userId);
                }
                _loggingService.Info("UserId Authenticated Failed: " + userId);
            }
            catch (Exception ex)
            {
                //Error
                _loggingService.Error("An error has occurred", ex);
            }
            //Fail
            return null;
        }

        public IEnumerable<CourseDto> GetAllCourseByUserId(string userId)
        {
            try
            {
                //Validate user
                if (_userRepository.IsAuthenticated(userId))
                {
                    //GetUserProfile
                    var courses = _courseRepository.GetAllCourseByUserId(userId);
                    if (courses != null)
                    {
                        //Success
                        return courses;
                    }
                    _loggingService.Info("Not profile for the user found: " + userId);
                }
                _loggingService.Info("UserId Authenticated Failed: " + userId);
            }
            catch (Exception ex)
            {
                //Error
                _loggingService.Error("An error has occurred", ex);
            }
            //Fail
            return null;
        }

        public bool InsertCourse(string userId, CourseDto course)
        {
            try
            {
                //Validate user
                if (_userRepository.IsAuthenticated(userId))
                {
                    //Validate Model
                    ICollection<ValidationResult> results;
                    if (IsValidModel(course, out results))
                    {
                        //Call Repository
                        if (_courseRepository.InsertCourse(course))
                        {
                            //Save
                            if (_courseRepository.Save())
                            {
                                //Success
                                return true;
                            }
                            _loggingService.Info("Failed To Save");
                        }
                        _loggingService.Info("UserRepository Failed Insert");
                    }
                    _loggingService.Info("Model Validation Failed: " + course);
                }
                _loggingService.Info("UserId Authenticated Failed: " + userId);
            }
            catch (Exception ex)
            {
                //Error
                _loggingService.Error("An error has occurred", ex);
            }
            //Fail
            return false;
        }

        public bool UpdateCourse(string userId, CourseDto course)
        {
            try
            {
                //Validate user
                if (_userRepository.IsAuthenticated(userId))
                {
                    var record = _courseRepository.GetCourseById(course.Id.ToString());
                    if (record != null)
                    {
                        //Validate Model
                        ICollection<ValidationResult> results;
                        if (IsValidModel(course, out results))
                        {
                            if (ModelCompareChecker.Compare(course, record))
                            {
                                return true;
                            }

                            record.Title = course.Title;
                            record.College = course.College;
                            record.Summary = course.Summary;
                            record.Location = course.Location;
                            record.StartDate = course.StartDate;
                            record.EndDate = course.EndDate;
                            record.IsCurrent = course.IsCurrent;

                            return _courseRepository.UpdateCourse(record);
                        }
                        _loggingService.Info("Model Validation Failed: " + course);
                    }
                }
                _loggingService.Info("UserId Authenticated Failed: " + userId);
            }
            catch (Exception ex)
            {
                //Error
                _loggingService.Error("An error has occurred", ex);
            }
            //Fail
            return false;
        }

        public bool DeleteCourse(string userId, string courseId)
        {
            try
            {
                //Validate user
                if (_userRepository.IsAuthenticated(userId))
                {
                    //GetUserProfile
                    var course = _courseRepository.GetCourseById(courseId);
                    if (course != null)
                    {
                        //Success
                        var isDeleted = _courseRepository.DeleteCourse(course.Id.ToString());
                        if (isDeleted)
                        {
                            return _courseRepository.Save();
                        }
                    }
                    _loggingService.Info("Not profile for the user found: " + userId);
                }
                _loggingService.Info("UserId Authenticated Failed: " + userId);
            }
            catch (Exception ex)
            {
                //Error
                _loggingService.Error("An error has occurred", ex);
            }
            //Fail
            return false;
        }
    }
}
