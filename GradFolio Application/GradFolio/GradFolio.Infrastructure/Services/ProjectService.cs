using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GradFolio.Core.DTO;
using GradFolio.Core.Repository;
using GradFolio.Core.Services;
using GradFolio.Infrastructure.Services.Helpers;

namespace GradFolio.Infrastructure.Services
{
    public class ProjectService : DataAnnotationsValidator, IProjectService
    {
        private readonly ILoggingService _loggingService;
        private readonly IProjectRepository _projectRepository;
        private readonly IUserRepository _userRepository;

        public ProjectService(ILoggingService loggingService, IProjectRepository projectRepository, IUserRepository userRepository)
        {
            _loggingService = loggingService;
            _projectRepository = projectRepository;
            _userRepository = userRepository;
        }

        public ProjectDto GetProjectById(string userId, string projectId)
        {
            try
            {
                //Validate user
                if (_userRepository.IsAuthenticated(userId))
                {
                    //GetUserProfile
                    var project = _projectRepository.GetProjectById(projectId);
                    if (project != null)
                    {
                        //Success
                        return project;
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

        public IEnumerable<ProjectDto> GetProjectList(string userId)
        {
            try
            {
                //Validate user
                if (_userRepository.IsAuthenticated(userId))
                {
                    //GetUserProfile
                    var projects = _projectRepository.GetAllProjectByUserId(userId);
                    if (projects != null)
                    {
                        //Success
                        return projects;
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

        public bool InsertProject(string userId, ProjectDto project)
        {
            try
            {
                //Validate user
                if (_userRepository.IsAuthenticated(userId))
                {
                    //Validate Model
                    ICollection<ValidationResult> results;
                    if (IsValidModel(project, out results))
                    {
                        //Call Repository
                        if (_projectRepository.InsertProject(project))
                        {
                            //Save
                            if (_projectRepository.Save())
                            {
                                //Success
                                return true;
                            }
                            _loggingService.Info("Failed To Save");
                        }
                        _loggingService.Info("UserRepository Failed Insert");
                    }
                    _loggingService.Info("Model Validation Failed: " + project);
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

        public bool UpdateProject(string userId, ProjectDto project)
        {
            try
            {
                //Validate user
                if (_userRepository.IsAuthenticated(userId))
                {
                    var record = _projectRepository.GetProjectById(project.Id.ToString()); ;
                    if (record != null)
                    {
                        project.CreateDate = record.CreateDate;
                        //Validate Model
                        ICollection<ValidationResult> results;
                        if (IsValidModel(project, out results))
                        {
                            project.CreateDate = record.CreateDate;
                            if (ModelCompareChecker.Compare(project, record))
                            {
                                return true;
                            }

                            record.Title = project.Title;
                            record.Summary = project.Summary;

                            return _projectRepository.UpdateProject(record);
                        }
                        _loggingService.Info("Model Validation Failed: " + project);
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

        public bool DeleteProject(string userId, string projectId)
        {
            try
            {
                //Validate user
                if (_userRepository.IsAuthenticated(userId))
                {
                    //GetUserProfile
                    var project = _projectRepository.GetProjectById(projectId);
                    if (project != null)
                    {
                        //Success
                        var isDeleted = _projectRepository.DeleteProject(project.Id.ToString());
                        if (isDeleted)
                        {
                            return _projectRepository.Save();
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




        //public ProjectViewModel GetProjectById(string userId, int projectId)
        //{
        //    return GetProjectList(userId).FirstOrDefault(x => x.Id == projectId);
        //}

        //public IEnumerable<ProjectViewModel> GetProjectList(string userId)
        //{
        //    var projectList = new List<ProjectViewModel>
        //    {
        //        new ProjectViewModel()
        //        {
        //            Id = 1,
        //            Type = "Web Application",
        //            Title = "Project 1",
        //            Summary = "This is project summary 1",
        //            CreateDate = DateTime.Now,
        //            UrlLink = "https://www.youtube.com/watch?v=E2LM3ZlcDnk",
        //            Resources = new List<ProjectResource>()
        //            {
        //                new ProjectResource()
        //                {
        //                    Id = 1,
        //                    Type = "image",
        //                    UrlLink =
        //                        "http://www.gettyimages.co.uk/gi-resources/images/Homepage/Category-Creative/UK/UK_Creative_462809583.jpg"
        //                },
        //                new ProjectResource()
        //                {
        //                    Id = 2,
        //                    Type = "image",
        //                    UrlLink =
        //                        "http://i.dailymail.co.uk/i/pix/2015/01/06/2473100D00000578-2898639-Photographer_Andrey_Gudkov_snapped_the_images_on_the_plains_of_t-a-20_1420546215677.jpg"
        //                }
        //            }
        //        },
        //        new ProjectViewModel()
        //        {
        //            Id = 2,
        //            Type = "Web Application",
        //            Title = "Project 2",
        //            Summary = "This is project summary 1",
        //            CreateDate = DateTime.Now,
        //            UrlLink = "https://www.youtube.com/watch?v=E2LM3ZlcDnk",
        //            Resources = new List<ProjectResource>()
        //            {
        //                new ProjectResource()
        //                {
        //                    Id = 3,
        //                    Type = "image",
        //                    UrlLink =
        //                        "http://www.gettyimages.co.uk/gi-resources/images/Homepage/Category-Creative/UK/UK_Creative_462809583.jpg"
        //                },
        //                new ProjectResource()
        //                {
        //                    Id = 4,
        //                    Type = "image",
        //                    UrlLink =
        //                        "http://i.dailymail.co.uk/i/pix/2015/01/06/2473100D00000578-2898639-Photographer_Andrey_Gudkov_snapped_the_images_on_the_plains_of_t-a-20_1420546215677.jpg"
        //                }
        //            }
        //        },
        //        new ProjectViewModel()
        //        {
        //            Id = 3,
        //            Type = "Web Application",
        //            Title = "Project 1",
        //            Summary = "This is project summary 1",
        //            CreateDate = DateTime.Now,
        //            UrlLink = "https://www.youtube.com/watch?v=E2LM3ZlcDnk",
        //            Resources = new List<ProjectResource>()
        //            {
        //                new ProjectResource()
        //                {
        //                    Id = 1,
        //                    Type = "image",
        //                    UrlLink =
        //                        "http://www.gettyimages.co.uk/gi-resources/images/Homepage/Category-Creative/UK/UK_Creative_462809583.jpg"
        //                },
        //                new ProjectResource()
        //                {
        //                    Id = 2,
        //                    Type = "image",
        //                    UrlLink =
        //                        "http://i.dailymail.co.uk/i/pix/2015/01/06/2473100D00000578-2898639-Photographer_Andrey_Gudkov_snapped_the_images_on_the_plains_of_t-a-20_1420546215677.jpg"
        //                }
        //            }
        //        },
        //        new ProjectViewModel()
        //        {
        //            Id = 4,
        //            Type = "Web Application",
        //            Title = "Project 2",
        //            Summary = "This is project summary 1",
        //            CreateDate = DateTime.Now,
        //            UrlLink = "https://www.youtube.com/watch?v=E2LM3ZlcDnk",
        //            Resources = new List<ProjectResource>()
        //            {
        //                new ProjectResource()
        //                {
        //                    Id = 3,
        //                    Type = "image",
        //                    UrlLink =
        //                        "http://www.gettyimages.co.uk/gi-resources/images/Homepage/Category-Creative/UK/UK_Creative_462809583.jpg"
        //                },
        //                new ProjectResource()
        //                {
        //                    Id = 4,
        //                    Type = "image",
        //                    UrlLink =
        //                        "http://i.dailymail.co.uk/i/pix/2015/01/06/2473100D00000578-2898639-Photographer_Andrey_Gudkov_snapped_the_images_on_the_plains_of_t-a-20_1420546215677.jpg"
        //                }
        //            }
        //        }
        //    };
        //    return projectList;
        //}

      
    }
}
