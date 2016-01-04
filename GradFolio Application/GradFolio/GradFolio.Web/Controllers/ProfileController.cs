using System;
using System.Web.Mvc;
using GradFolio.Core.DTO;
using GradFolio.Core.Services;
using GradFolio.Core.ViewModels;
using Microsoft.AspNet.Identity;

namespace GradFolio.Web.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly ILoggingService _loggingService;
        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService,
            ILoggingService loggingService)
        {
            _profileService = profileService;
            _loggingService = loggingService;
        }

        [HttpGet]
        public ActionResult InitialProfileSetup()
        {
            var profile = _profileService.GetUserProfile(User.Identity.GetUserId());
            if (profile == null)
            {
                return View();
            }
            return RedirectToAction("Overview", "Portal");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InitialProfileSetup(ProfileViewModel profile)
        {
            if (ModelState.IsValid)
            {
                var insert = new ProfileDto()
                {
                    UserId = User.Identity.GetUserId(),
                    FirstName = profile.FirstName,
                    LastName = profile.LastName,
                    Title = profile.Title,
                    Summary = profile.Summary,
                    Location = profile.Location,
                    Mobile = profile.Mobile,
                    Phone = profile.Phone,
                    IsAvailable = profile.IsAvailable,
                    AvailableFromDate = profile.AvailableFromDate,
                    ImageUrl = profile.ImageUrl,
                    PortfolioUrl = profile.PortfolioUrl,
                    LinkedInUrl = profile.LinkedInUrl
                };

                var isAdded = _profileService.InsertProfile(User.Identity.GetUserId(), insert);
                if (isAdded)
                {
                    return RedirectToAction("Overview", "Portal");
                }
            }
            return View(profile);
        }


        [HttpGet]
        public ActionResult ViewProfile()
        {
            try
            {
                var profile = GetProfileViewModel(User.Identity.GetUserId());
                return PartialView("_ViewProfile", profile);
            }
            catch (Exception ex)
            {
                _loggingService.Error("An error has occurred", ex);
            }
            return Content("Item not found");
        }

        [HttpGet]
        public ActionResult EditProfile()
        {
            try
            {
                var profile = GetProfileViewModel(User.Identity.GetUserId());
                return PartialView("_EditProfile", profile);
            }
            catch (Exception ex)
            {
                _loggingService.Error("An error has occurred", ex);
            }
            return Content("Item not found");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfile(ProfileViewModel profile)
        {
            if (ModelState.IsValid)
            {
                var isUpadated = UpdateProfile(User.Identity.GetUserId(), profile);

                if (isUpadated)
                {
                    return Json(new {success = true});
                }
            }
            return PartialView("_EditProfile", profile);
        }


        private ProfileViewModel GetProfileViewModel(string userId)
        {
            var profile = _profileService.GetUserProfile(userId);


            var result = new ProfileViewModel()
            {
                Id = profile.Id,
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                Title = profile.Title,
                Summary = profile.Summary,
                Location = profile.Location,
                Email = User.Identity.GetUserName(),
                Mobile = profile.Mobile,
                Phone = profile.Phone,
                JoinDate = profile.JoinDate,
                IsAvailable = profile.IsAvailable,
                AvailableFromDate = profile.AvailableFromDate,
                ImageUrl = profile.ImageUrl,
                PortfolioUrl = profile.PortfolioUrl,
                LinkedInUrl = profile.LinkedInUrl
            };
            return result;
        }

        private bool UpdateProfile(string userId, ProfileViewModel profile)
        {
            var update = new ProfileDto()
            {
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                Title = profile.Title,
                Summary = profile.Summary,
                Location = profile.Location,
                Mobile = profile.Mobile,
                Phone = profile.Phone,
                IsAvailable = profile.IsAvailable,
                AvailableFromDate = profile.AvailableFromDate,
                ImageUrl = profile.ImageUrl,
                LinkedInUrl = profile.LinkedInUrl
            };

            return _profileService.UpdateProfile(userId, update);
        }
    }
}