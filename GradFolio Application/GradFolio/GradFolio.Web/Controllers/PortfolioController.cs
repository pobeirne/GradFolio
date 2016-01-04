using System.Web.Mvc;
using GradFolio.Core.DTO;
using GradFolio.Core.Services;
using GradFolio.Core.ViewModels;
using Microsoft.AspNet.Identity;

namespace GradFolio.Web.Controllers
{
    [Authorize]
    public class PortfolioController : Controller
    {
        //public string UserId = "d5390018-e4ba-4ae4-add2-aaaa634f1a92";

        private readonly ILoggingService _loggingService;
        private readonly IPortalService _portalService;
        private readonly IPortfolioService _portfolioService;

        public PortfolioController(
            IPortalService portalService,
            IPortfolioService portfolioService,
            ILoggingService loggingService)
        {
            _portalService = portalService;
            _portfolioService = portfolioService;
            _loggingService = loggingService;
        }

        [HttpGet]
        public ActionResult PortfolioOverview()
        {
            var overview = GetOverview(User.Identity.GetUserId());
            return PartialView("_PortfolioOverview", overview);
        }


        [HttpGet]
        public ActionResult AddPortfolio()
        {
            return PartialView("_AddPortfolio");
        }

        [HttpPost]
        [ActionName("AddPortfolio")]
        public ActionResult AddPortfolioPost()
        {
            var portfolio = new PortfolioDto()
            {
                Type = "Template1",
                UserId = User.Identity.GetUserId()
            };
            var isAdded = _portfolioService.InsertPortfolio(portfolio, User.Identity.GetUserId());

            return Json(ModelState.IsValid ? new {success = true} : new {success = false});
        }


        [HttpGet]
        public ActionResult RemovePortfolio(string itemId)
        {
            var portfolio = GetOverview(User.Identity.GetUserId()).Portfolio;
            return PartialView("_RemovePortfolio", portfolio);
        }

        [HttpPost]
        [ActionName("RemovePortfolio")]
        public ActionResult RemovePortfolioPost(string id)
        {
            {
                var isDeleted = _portfolioService.DeletePortfolio(User.Identity.GetUserId(), id);
                if (isDeleted)
                {
                    return Json(new {success = true});
                }
            }
            var portfolio = GetOverview(User.Identity.GetUserId()).Portfolio;
            ModelState.AddModelError(string.Empty, "The item cannot be removed");
            return PartialView("_RemovePortfolio", portfolio);
        }


        private PortfolioViewModel GetOverview(string userId)
        {
            var overview = new PortfolioViewModel
            {
                Stats = CheckStatus(userId),
                Portfolio = GetPortfolio(userId)
            };

            if (overview.Stats.ProfileCount == 1 && overview.Stats.ExperienceCount >= 2 &&
                overview.Stats.CourseCount >= 2 && overview.Stats.SkillCount >= 2
                && overview.Stats.AwardCount >= 2 && overview.Stats.InterestCount >= 2
                && overview.Stats.ProjectCount >= 2 && overview.Stats.CvCount >=0)
            {
                overview.IsReady = true;
            }
            return overview;
        }

        private PortfolioModel GetPortfolio(string userId)
        {
            var query = _portfolioService.GetPortfolioByUserId(userId);
            if (query != null)
            {
                return new PortfolioModel
                {
                    Id = query.Id,
                    Type = query.Type,
                    RefNum = query.RefNum,
                    CreateDate = query.CreateDate
                };
            }
            return null;
        }

        private PortalOverviewStats CheckStatus(string userId)
        {
            var query = _portalService.GetPortalOverview(userId);
            if (query == null) return null;

            var stats = new PortalOverviewStats
            {
                ProfileCount = query.ProfileCount,
                ExperienceCount = query.ExperienceCount,
                CourseCount = query.CourseCount,
                SkillCount = query.SkillCount,
                AwardCount = query.AwardCount,
                InterestCount = query.InterestCount,
                ProjectCount = query.ProjectCount,
                PortfolioCount = query.PortfolioCount
            };
            return stats;
        }
    }
}

