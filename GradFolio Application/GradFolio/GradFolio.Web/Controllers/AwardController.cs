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
    public class AwardController : Controller
    {
        //public string UserId = "d5390018-e4ba-4ae4-add2-aaaa634f1a92";

        private readonly ILoggingService _loggingService;
        private readonly IAwardService _awardService;

        public AwardController(
            IAwardService awardService,
            ILoggingService loggingService)
        {
            _awardService = awardService;
            _loggingService = loggingService;
        }


        [HttpGet]
        public ActionResult ViewAwardList(string sort)
        {
            try
            {
                var awardList = GetAwardList(User.Identity.GetUserId());
                if (awardList != null)
                {
                    return PartialView("_ViewAwardList", awardList);
                }
            }
            catch (Exception ex)
            {
                _loggingService.Error("An error has occurred", ex);
            }
            return Content("Item not found");
        }

        [HttpGet]
        public ActionResult ViewAward(string itemId)
        {
            var award = GetAward(User.Identity.GetUserId(), itemId);
            return PartialView("_ViewAward", award);
        }

        [HttpGet]
        public ActionResult AddAward()
        {
            return PartialView("_AddAward");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddAward(AwardViewModel award)
        {
            if (ModelState.IsValid)
            {
                var isAdded = AddAward(User.Identity.GetUserId(), award);
                if (isAdded)
                {
                    return Json(new {success = true});
                }
            }
            return PartialView("_AddAward", award);
        }


        [HttpGet]
        public ActionResult EditAward(string itemId)
        {
            var award = GetAward(User.Identity.GetUserId(), itemId);
            return PartialView("_EditAward", award);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAward(AwardViewModel award)
        {
            if (ModelState.IsValid)
            {
                var isUpdated = UpdateAward(User.Identity.GetUserId(), award);
                if (isUpdated)
                {
                    return Json(new {success = true});
                }
            }
            return PartialView("_EditAward", award);
        }


        [HttpGet]
        public ActionResult RemoveAward(string itemId)
        {
            var award = GetAward(User.Identity.GetUserId(), itemId);
            return PartialView("_RemoveAward", award);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("RemoveAward")]
        public ActionResult RemoveAwardPost(string id)
        {
            {
                var isDeleted = _awardService.DeleteAward(User.Identity.GetUserId(), id);
                if (isDeleted)
                {
                    return Json(new {success = true});
                }
            }
            var award = GetAward(User.Identity.GetUserId(), id);
            ModelState.AddModelError(string.Empty, "The item cannot be removed");
            return PartialView("_RemoveAward", award);
        }


        private IEnumerable<AwardViewModel> GetAwardList(string userId)
        {
            var awards = _awardService.GetAllAwardByUserId(userId);
            if (awards == null) return null;


            var result = awards.Select(award => new AwardViewModel
            {
                Id = award.Id,
                Title = award.Title,
                Level = award.Level,
                IssuedBy = award.IssuedBy,
                IssuedDate = award.IssuedDate
            }).ToList();

            return result;
        }


        private AwardViewModel GetAward(string userId, string awardId)
        {
            var award = _awardService.GetAwardById(userId, awardId);
            if (award == null) return null;


            var result = new AwardViewModel()
            {
                Id = award.Id,
                Title = award.Title,
                Level = award.Level,
                IssuedBy = award.IssuedBy,
                IssuedDate = award.IssuedDate
            };

            return result;
        }


        private bool AddAward(string userId, AwardViewModel award)
        {
            var addRecord = new AwardDto()
            {
                UserId = userId,
                Title = award.Title,
                Level = award.Level,
                IssuedBy = award.IssuedBy,
                IssuedDate = award.IssuedDate
            };
            return _awardService.InsertAward(userId, addRecord);
        }

        private bool UpdateAward(string userId, AwardViewModel award)
        {
            var updateRecord = new AwardDto()
            {
                Id = award.Id,
                UserId = userId,
                Title = award.Title,
                Level = award.Level,
                IssuedBy = award.IssuedBy,
                IssuedDate = award.IssuedDate
            };
            return _awardService.UpdateAward(userId, updateRecord);
        }
    }
}