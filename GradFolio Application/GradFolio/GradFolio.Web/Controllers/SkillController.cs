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
    public class SkillController : Controller
    {
        //public string UserId = "d5390018-e4ba-4ae4-add2-aaaa634f1a92";

        private readonly ILoggingService _loggingService;
        private readonly ISkillService _skillService;

        public SkillController(
            ISkillService skillService,
            ILoggingService loggingService)
        {
            _skillService = skillService;
            _loggingService = loggingService;
        }


        [HttpGet]
        public ActionResult ViewSkillList(string sort)
        {
            try
            {
                var skillList = GetSkillList(User.Identity.GetUserId());

                if (skillList != null)
                {
                    return PartialView("_ViewSkillList", skillList);
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
        public ActionResult ViewSkill(string itemId)
        {
            var skill = GetSkill(User.Identity.GetUserId(), itemId);
            return PartialView("_ViewSkill", skill);
        }

        [HttpGet]
        public ActionResult AddSkill()
        {
            return PartialView("_AddSkill");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddSkill(SkillViewModel skill)
        {
            if (ModelState.IsValid)
            {
                var isAdded = AddSkill(User.Identity.GetUserId(), skill);
                if (isAdded)
                {
                    return Json(new {success = true});
                }
            }
            return PartialView("_AddSkill", skill);
        }


        [HttpGet]
        public ActionResult EditSkill(string itemId)
        {
            var skill = GetSkill(User.Identity.GetUserId(), itemId);
            return PartialView("_EditSkill", skill);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditSkill(SkillViewModel skill)
        {
            if (ModelState.IsValid)
            {
                var isUpdated = UpdateSkill(User.Identity.GetUserId(), skill);
                if (isUpdated)
                {
                    return Json(new {success = true});
                }
            }
            return PartialView("_EditSkill", skill);
        }


        [HttpGet]
        public ActionResult RemoveSkill(string itemId)
        {
            var skill = GetSkill(User.Identity.GetUserId(), itemId);
            return PartialView("_RemoveSkill", skill);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("RemoveSkill")]
        public ActionResult RemoveSkillPost(string id)
        {
            {
                var isDeleted = _skillService.DeleteSkill(User.Identity.GetUserId(), id);
                if (isDeleted)
                {
                    return Json(new {success = true});
                }
            }
            var skill = GetSkill(User.Identity.GetUserId(), id);
            ModelState.AddModelError(string.Empty, "The item cannot be removed");
            return PartialView("_RemoveSkill", skill);
        }


        private IEnumerable<SkillViewModel> GetSkillList(string userId)
        {
            var skills = _skillService.GetAllSkillByUserId(userId);
            if (skills == null) return null;


            var result = skills.Select(skill => new SkillViewModel()
            {
                Id = skill.Id,
                Title = skill.Title,
                Summary = skill.Summary,
                CreateDate = skill.CreateDate
            }).ToList();

            return result;
        }

        private SkillViewModel GetSkill(string userId, string skillId)
        {
            var skill = _skillService.GetSkillById(userId, skillId);
            if (skill == null) return null;

            var result = new SkillViewModel()
            {
                Id = skill.Id,
                Title = skill.Title,
                Level = skill.Level,
                Summary = skill.Summary,
                CreateDate = skill.CreateDate
            };

            return result;
        }

        private bool AddSkill(string userId, SkillViewModel skill)
        {
            var addRecord = new SkillDto()
            {
                UserId = userId,
                Title = skill.Title,
                Level = skill.Level,
                Summary = skill.Summary
            };
            return _skillService.InsertSkill(userId, addRecord);
        }

        private bool UpdateSkill(string userId, SkillViewModel skill)
        {
            var updateRecord = new SkillDto()
            {
                Id = skill.Id,
                UserId = userId,
                Title = skill.Title,
                Level = skill.Level,
                Summary = skill.Summary,
                CreateDate = skill.CreateDate
            };
            return _skillService.UpdateSkill(userId, updateRecord);
        }
    }
}