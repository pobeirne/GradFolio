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
    public class ProjectController : Controller
    {
        //public string UserId = "d5390018-e4ba-4ae4-add2-aaaa634f1a92";

        private readonly ILoggingService _loggingService;
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService, ILoggingService loggingService)
        {
            _projectService = projectService;
            _loggingService = loggingService;
        }

        [HttpGet]
        public ActionResult ViewProjectList(string sort)
        {
            try
            {
                var projectList = GetProjectList(User.Identity.GetUserId());
                if (projectList != null)
                {
                    return PartialView("_ViewProjectList", projectList);
                }
            }
            catch (Exception ex)
            {
                _loggingService.Error("An error has occurred", ex);
            }
            return Content("Item not found");
        }

        [HttpGet]
        public ActionResult ViewProject(string itemId)
        {
            var project = GetProject(User.Identity.GetUserId(), itemId);
            return PartialView("_ViewProject", project);
        }

        [HttpGet]
        public ActionResult AddProject()
        {
            return PartialView("_AddProject");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddProject(ProjectViewModel project)
        {
            if (ModelState.IsValid)
            {
                var isAdded = AddProject(User.Identity.GetUserId(), project);
                if (isAdded)
                {
                    return Json(new {success = true});
                }
            }
            return PartialView("_AddProject", project);
        }


        [HttpGet]
        public ActionResult EditProject(string itemId)
        {
            var project = GetProject(User.Identity.GetUserId(), itemId);
            return PartialView("_EditProject", project);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProject(ProjectViewModel project)
        {
            if (ModelState.IsValid)
            {
                var isUpdated = UpdateProject(User.Identity.GetUserId(), project);
                if (isUpdated)
                {
                    return Json(new {success = true});
                }
            }

            return PartialView("_EditProject", project);
        }


        [HttpGet]
        public ActionResult RemoveProject(string itemId)
        {
            var project = GetProject(User.Identity.GetUserId(), itemId);
            return PartialView("_RemoveProject", project);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("RemoveProject")]
        public ActionResult RemoveProjectPost(string id)
        {
            {
                var isDeleted = _projectService.DeleteProject(User.Identity.GetUserId(), id);
                if (isDeleted)
                {
                    return Json(new {success = true});
                }
            }
            var project = GetProject(User.Identity.GetUserId(), id);
            ModelState.AddModelError(string.Empty, "The item cannot be removed");
            return PartialView("_RemoveProject", project);
        }


        private IEnumerable<ProjectViewModel> GetProjectList(string userId)
        {
            var projects = _projectService.GetProjectList(userId);
            if (projects == null) return null;


            var result = projects.Select(project => new ProjectViewModel
            {
                Id = project.Id,
                Title = project.Title,
                Summary = project.Summary,
                CreateDate = project.CreateDate
            }).ToList();

            return result;
        }


        private ProjectViewModel GetProject(string userId, string projectId)
        {
            var project = _projectService.GetProjectById(userId, projectId);
            if (project == null) return null;


            var result = new ProjectViewModel()
            {
                Id = project.Id,
                Title = project.Title,
                Summary = project.Summary,
                CreateDate = project.CreateDate
            };

            return result;
        }


        private bool AddProject(string userId, ProjectViewModel project)
        {
            var addRecord = new ProjectDto()
            {
                UserId = userId,
                Title = project.Title,
                Summary = project.Summary
            };
            return _projectService.InsertProject(userId, addRecord);
        }

        private bool UpdateProject(string userId, ProjectViewModel project)
        {
            var updateRecord = new ProjectDto()
            {
                Id = project.Id,
                UserId = userId,
                Title = project.Title,
                Summary = project.Summary
            };
            return _projectService.UpdateProject(userId, updateRecord);
        }
    }
}