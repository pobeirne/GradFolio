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
    public class ExperienceController : Controller
    {
        //public string UserId = "d5390018-e4ba-4ae4-add2-aaaa634f1a92";

        private readonly ILoggingService _loggingService;
        private readonly IExperienceService _experienceService;

        public ExperienceController(
            IExperienceService experienceService,
            ILoggingService loggingService)
        {
            _experienceService = experienceService;
            _loggingService = loggingService;
        }

        [HttpGet]
        public ActionResult ViewExperienceList(string sort)
        {
            try
            {
                var experienceList = GetExperienceList(User.Identity.GetUserId());
                if (experienceList != null)
                {
                    return PartialView("_ViewExperienceList", experienceList);
                }
            }
            catch (Exception ex)
            {
                _loggingService.Error("An error has occurred", ex);
            }
            return Content("Item not found");
        }

        [HttpGet]
        public ActionResult ViewExperience(string itemId)
        {
            var experience = GetExperience(User.Identity.GetUserId(), itemId);
            return PartialView("_ViewExperience", experience);
        }

        [HttpGet]
        public ActionResult AddExperience()
        {
            return PartialView("_AddExperience");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddExperience(ExperienceViewModel experience)
        {
            if (ModelState.IsValid)
            {
                var isAdded = AddExperience(User.Identity.GetUserId(), experience);
                if (isAdded)
                {
                    return Json(new {success = true});
                }
            }
            return PartialView("_AddExperience", experience);
        }

        [HttpGet]
        public ActionResult EditExperience(string itemId)
        {
            var experience = GetExperience(User.Identity.GetUserId(), itemId);
            return PartialView("_EditExperience", experience);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditExperience(ExperienceViewModel experience)
        {
            if (ModelState.IsValid)
            {
                var isUpdated = UpdateExperience(User.Identity.GetUserId(), experience);
                if (isUpdated)
                {
                    return Json(new {success = true});
                }
            }
            return PartialView("_EditExperience", experience);
        }

        [HttpGet]
        public ActionResult RemoveExperience(string itemId)
        {
            var experience = GetExperience(User.Identity.GetUserId(), itemId);
            return PartialView("_RemoveExperience", experience);
        }

        [HttpPost]
        [ActionName("RemoveExperience")]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveExperiencePost(string id)
        {
            {
                var isDeleted = _experienceService.DeleteExperience(User.Identity.GetUserId(), id);
                if (isDeleted)
                {
                    return Json(new {success = true});
                }
            }
            var experience = GetExperience(User.Identity.GetUserId(), id);
            ModelState.AddModelError(string.Empty, "The item cannot be removed");
            return PartialView("_RemoveExperience", experience);
        }


        private IEnumerable<ExperienceViewModel> GetExperienceList(string userId)
        {
            var experiences = _experienceService.GetAllExperienceByUserId(userId);
            if (experiences == null) return null;


            var result = experiences.Select(experience => new ExperienceViewModel
            {
                Id = experience.Id,
                Title = experience.Title,
                Company = experience.Company,
                Summary = experience.Summary,
                Location = experience.Location,
                StartDate = experience.StartDate,
                EndDate = experience.EndDate,
                IsCurrent = experience.IsCurrent
            }).ToList();

            return result;
        }


        private ExperienceViewModel GetExperience(string userId, string experienceId)
        {
            var experience = _experienceService.GetExperienceById(userId, experienceId);
            if (experience == null) return null;


            var result = new ExperienceViewModel()
            {
                Id = experience.Id,
                Title = experience.Title,
                Company = experience.Company,
                Summary = experience.Summary,
                Location = experience.Location,
                StartDate = experience.StartDate,
                EndDate = experience.EndDate,
                IsCurrent = experience.IsCurrent
            };

            return result;
        }


        private bool AddExperience(string userId, ExperienceViewModel experience)
        {
            var addRecord = new ExperienceDto()
            {
                UserId = userId,
                Title = experience.Title,
                Company = experience.Company,
                Summary = experience.Summary,
                Location = experience.Location,
                StartDate = experience.StartDate,
                EndDate = experience.EndDate,
                IsCurrent = experience.IsCurrent
            };
            return _experienceService.InsertExperience(userId, addRecord);
        }

        private bool UpdateExperience(string userId, ExperienceViewModel experience)
        {
            var updateRecord = new ExperienceDto()
            {
                Id = experience.Id,
                UserId = userId,
                Title = experience.Title,
                Company = experience.Company,
                Summary = experience.Summary,
                Location = experience.Location,
                StartDate = experience.StartDate,
                EndDate = experience.EndDate,
                IsCurrent = experience.IsCurrent
            };
            return _experienceService.UpdateExperience(userId, updateRecord);
        }
    }
}