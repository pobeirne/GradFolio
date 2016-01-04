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
    public class InterestController : Controller
    {
        //public string UserId = "d5390018-e4ba-4ae4-add2-aaaa634f1a92";

        private readonly ILoggingService _loggingService;
        private readonly IInterestService _interestService;

        public InterestController(
            IInterestService interestService,
            ILoggingService loggingService)
        {
            _interestService = interestService;
            _loggingService = loggingService;
        }

        [HttpGet]
        public ActionResult ViewInterestList(string sort)
        {
            try
            {
                var interestList = GetInterestList(User.Identity.GetUserId());
                if (interestList != null)
                {
                    return PartialView("_ViewInterestList", interestList);
                }
            }
            catch (Exception ex)
            {
                _loggingService.Error("An error has occurred", ex);
            }
            return Content("Item not found");
        }

        [HttpGet]
        public ActionResult ViewInterest(string itemId)
        {
            var interest = GetInterest(User.Identity.GetUserId(), itemId);
            return PartialView("_ViewInterest", interest);
        }

        [HttpGet]
        public ActionResult AddInterest()
        {
            return PartialView("_AddInterest");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddInterest(InterestViewModel interest)
        {
            if (ModelState.IsValid)
            {
                var isAdded = AddInterest(User.Identity.GetUserId(), interest);
                if (isAdded)
                {
                    return Json(new {success = true});
                }
            }

            return PartialView("_AddInterest", interest);
        }


        [HttpGet]
        public ActionResult EditInterest(string itemId)
        {
            var interest = GetInterest(User.Identity.GetUserId(), itemId);
            return PartialView("_EditInterest", interest);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditInterest(InterestViewModel interest)
        {
            if (ModelState.IsValid)
            {
                var isUpdated = UpdateInterest(User.Identity.GetUserId(), interest);
                if (isUpdated)
                {
                    return Json(new {success = true});
                }
            }

            return PartialView("_EditInterest", interest);
        }


        [HttpGet]
        public ActionResult RemoveInterest(string itemId)
        {
            var interest = GetInterest(User.Identity.GetUserId(), itemId);
            return PartialView("_RemoveInterest", interest);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("RemoveInterest")]
        public ActionResult RemoveInterestPost(string id)
        {
            {
                var isDeleted = _interestService.DeleteInterest(User.Identity.GetUserId(), id);
                if (isDeleted)
                {
                    return Json(new {success = true});
                }
            }
            var interest = GetInterest(User.Identity.GetUserId(), id);
            ModelState.AddModelError(string.Empty, "The item cannot be removed");
            return PartialView("_RemoveInterest", interest);
        }


        private IEnumerable<InterestViewModel> GetInterestList(string userId)
        {
            var interests = _interestService.GetAllInterestByUserId(userId);
            if (interests == null) return null;


            var result = interests.Select(interest => new InterestViewModel
            {
                Id = interest.Id,
                Title = interest.Title,
                Summary = interest.Summary,
                CreateDate = interest.CreateDate
            }).ToList();

            return result;
        }


        private InterestViewModel GetInterest(string userId, string interestId)
        {
            var interest = _interestService.GetInterestById(userId, interestId);
            if (interest == null) return null;


            var result = new InterestViewModel()
            {
                Id = interest.Id,
                Title = interest.Title,
                Summary = interest.Summary,
                CreateDate = interest.CreateDate
            };

            return result;
        }


        private bool AddInterest(string userId, InterestViewModel interest)
        {
            var addRecord = new InterestDto()
            {
                UserId = userId,
                Title = interest.Title,
                Summary = interest.Summary
            };
            return _interestService.InsertInterest(userId, addRecord);
        }

        private bool UpdateInterest(string userId, InterestViewModel interest)
        {
            var updateRecord = new InterestDto()
            {
                Id = interest.Id,
                UserId = userId,
                Title = interest.Title,
                Summary = interest.Summary
            };
            return _interestService.UpdateInterest(userId, updateRecord);
        }
    }
}