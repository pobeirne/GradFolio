using System;
using System.Web.Mvc;
using GradFolio.Core.Services;
using GradFolio.Core.ViewModels;
using Microsoft.AspNet.Identity;

namespace GradFolio.Web.Controllers
{
    [Authorize]
    public class PortalController : Controller
    {
        //public string UserId = "d5390018-e4ba-4ae4-add2-aaaa634f1a92";
        private readonly ILoggingService _loggingService;
        private readonly IPortalService _portalService;

        public PortalController(
            IPortalService portalService,
            ILoggingService loggingService)
        {
            _portalService = portalService;
            _loggingService = loggingService;
        }


        [HttpGet]
        public ActionResult Overview()
        {
            return View();
        }

        [HttpGet]
        public ActionResult PortalOverview()
        {
            var overview = GetOverview(User.Identity.GetUserId());
            return PartialView("_PortalOverview", overview);
        }

        [HttpGet]
        public ActionResult ProfileInfo()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Experience()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Courses()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Skills()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Projects()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Awards()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Interests()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CurriculumVitae()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Portfolio()
        {
            return View();
        }


        public PortalOverviewViewModel GetOverview(string userId)
        {
            var query = _portalService.GetPortalOverview(userId);
            if (query == null) return null;


            var overview = new PortalOverviewViewModel
            {
                PortalOverviewStats = new PortalOverviewStats
                {
                    ExperienceCount = query.ExperienceCount,
                    CourseCount = query.CourseCount,
                    SkillCount = query.SkillCount,
                    AwardCount = query.AwardCount,
                    InterestCount = query.InterestCount,
                    ProjectCount = query.ProjectCount
                }
            };


            if (query.Profile != null)
            {
                overview.ProfileOverview = new ProfileOverview
                {
                    Name = query.Profile.FirstName + " " + query.Profile.LastName,
                    Title = query.Profile.Title,
                    ImageUrl = query.Profile.ImageUrl,
                    JoinDate = query.Profile.JoinDate
                };
            }

            if (query.LatestExperience != null)
            {
                overview.ExperienceOverview = new ExperienceOverview
                {
                    Title = query.LatestExperience.Title,
                    Company = query.LatestExperience.Company,
                    CreateDate = query.LatestExperience.CreateDate
                };
            }

            if (query.LatestCourse != null)
            {
                overview.CourseOverview = new CourseOverview
                {
                    Id = query.LatestCourse.Id,
                    Title = query.LatestCourse.Title,
                    College = query.LatestCourse.College,
                    CreateDate = DateTime.Now
                };
            }

            if (query.LatestSkill != null)
            {
                overview.SkillOverview = new SkillOverview
                {
                    Id = query.LatestSkill.Id,
                    Title = query.LatestSkill.Title,
                    Level = query.LatestSkill.Level,
                    CreateDate = query.LatestSkill.CreateDate
                };
            }

            if (query.LatestAward != null)
            {
                overview.AwardOverview = new AwardOverview
                {
                    Id = query.LatestAward.Id,
                    Title = query.LatestAward.Title,
                    Level = query.LatestAward.Level,
                    CreateDate = query.LatestAward.CreateDate
                };
            }

            if (query.LatestInterest != null)
            {
                overview.InterestOverview = new InterestOverview
                {
                    Id = query.LatestInterest.Id,
                    Title = query.LatestInterest.Title,
                    CreateDate = query.LatestInterest.CreateDate
                };
            }

            if (query.LatestProject != null)
            {
                overview.ProjectOverview = new ProjectOverview
                {
                    Id = query.LatestProject.Id,
                    Title = query.LatestProject.Title,
                    CreateDate = query.LatestProject.CreateDate
                };
            }


            if (query.LatestCv != null)
            {
                overview.CvOverview = new CvOverview
                {
                    Id = query.LatestCv.Id,
                    FileName = query.LatestCv.Name,
                    Type = query.LatestCv.Type,
                    CreateDate = query.LatestCv.CreateDate
                };
            }

            if (query.PortfolioCount > 0)
            {
                overview.PortfolioOverview = new PortfolioOverview
                {
                    Id = query.LatestPortfolio.Id,
                    Type = query.LatestPortfolio.Type,
                    PortfolioUrl = query.LatestPortfolio.RefNum.ToString(),
                    CreateDate = query.LatestPortfolio.CreateDate
                };
            }

            //need work


            return overview;
        }
    }
}