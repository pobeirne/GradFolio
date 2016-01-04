using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using GradFolio.Core.DTO;
using GradFolio.Core.Services;
using GradFolio.Core.ViewModels;
using Microsoft.AspNet.Identity;
using MvcRazorToPdf;

namespace GradFolio.Web.Controllers
{
    [Authorize]
    public class CvController : Controller
    {
        //public string UserId = "d5390018-e4ba-4ae4-add2-aaaa634f1a92";

        private readonly ILoggingService _loggingService;
        private readonly ICurriculumVitaeService _curriculumVitaeService;
        private readonly IExperienceService _experienceService;
        private readonly ICourseService _courseService;
        private readonly IPortalService _portalService;


        public CvController(
            ILoggingService loggingService,
            ICurriculumVitaeService curriculumVitaeService,
            IExperienceService experienceService,
            ICourseService courseService,
            IPortalService portalService)
        {
            _loggingService = loggingService;
            _curriculumVitaeService = curriculumVitaeService;
            _experienceService = experienceService;
            _courseService = courseService;
            _portalService = portalService;
        }


        [HttpGet]
        public ActionResult ViewCvList(string sort)
        {
            var overview = GetOverview(User.Identity.GetUserId());
            return PartialView("_ViewCVList", overview);
        }


        [HttpGet]
        public ActionResult AddCv()
        {
            return PartialView("_AddCV");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCv(string filename)
        {
            if (ModelState.IsValid)
            {
                var gencv = _curriculumVitaeService.GenerateCurriculumVitae(User.Identity.GetUserId(), filename);

                if (gencv)
                {
                    return Json(new {success = true});
                }
            }
            return PartialView("_AddCV");
        }

        [HttpGet]
        public ActionResult EditCv(string itemId)
        {
            var cvmodel = GetCurriculumVitae(User.Identity.GetUserId(), itemId);

            return PartialView("_EditCV", cvmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCv(CvFormViewModel cv)
        {
            if (ModelState.IsValid)
            {
                var isUpdated = UpdateCurriculumVitae(User.Identity.GetUserId(), cv);
                if (isUpdated)
                {
                    return Json(new {success = true});
                }
            }

            var cvmodel = GetCurriculumVitae(User.Identity.GetUserId(), cv.Id.ToString());
            return PartialView("_EditCV", cvmodel);
        }

        [HttpGet]
        public ActionResult RemoveCv(string itemId)
        {
            var cv = GetCurriculumVitae(User.Identity.GetUserId(), itemId);
            return PartialView("_RemoveCV", cv);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("RemoveCv")]
        public ActionResult RemoveCvPost(string id)
        {
            {
                var isDeleted = _curriculumVitaeService.DeleteCurriculumVitae(User.Identity.GetUserId(), id);
                if (isDeleted)
                {
                    return Json(new {success = true});
                }
            }
            var cv = GetCurriculumVitae(User.Identity.GetUserId(), id);
            ModelState.AddModelError(string.Empty, "The item cannot be removed");
            return PartialView("_RemoveCV", cv);
        }


        [HttpGet]
        [AllowAnonymous]
        public ActionResult ViewCv(string id)
        {
            var template = GeCvTemplate(id);

            return new PdfActionResult(template);
        }

        private CvFormViewModel GetCurriculumVitae(string userId, string cvId)
        {
            var cv = _curriculumVitaeService.GetCurriculumVitaeById(userId, cvId);
            if (cv == null) return null;


            var result = new CvFormViewModel()
            {
                Id = cv.Id,
                FileName = cv.Name,
                Type = cv.Type,
                RefNum = cv.RefNum,
                SelectedExp1 = cv.Experience1,
                SelectedExp2 = cv.Experience2,
                Experiences = GetExperienceSelectList(userId),
                SelectedCourse1 = cv.Course1,
                SelectedCourse2 = cv.Course2,
                Courses = GetCourseSelectList(userId),
                CreateDate = cv.CreateDate
            };

            return result;
        }

        private bool UpdateCurriculumVitae(string userId, CvFormViewModel cv)
        {
            var updateRecord = new CurriculumVitaeDto()
            {
                Id = cv.Id,
                UserId = userId,
                Name = cv.FileName,
                Type = "BASIC",
                RefNum = cv.RefNum,
                Experience1 = cv.SelectedExp1,
                Experience2 = cv.SelectedExp2,
                Course1 = cv.SelectedCourse1,
                Course2 = cv.SelectedCourse2
            };
            return _curriculumVitaeService.UpdateCurriculumVitae(userId, updateRecord);
        }

        private IEnumerable<CvFormViewModel> GetCvList(string userId)
        {
            var cvs = _curriculumVitaeService.GetAllCurriculumVitaeByUserId(userId);
            if (cvs == null) return null;

            var result = cvs.Select(cv => new CvFormViewModel()
            {
                Id = cv.Id,
                FileName = cv.Name,
                Type = cv.Type,
                RefNum = cv.RefNum,
                SelectedExp1 = cv.Experience1,
                SelectedExp2 = cv.Experience2,
                SelectedCourse1 = cv.Course1,
                SelectedCourse2 = cv.Course2,
                CreateDate = cv.CreateDate
            }).ToList();

            return result;
        }

        private CvOverviewViewModel GetOverview(string userId)
        {
            var overview = new CvOverviewViewModel
            {
                Stats = CheckStatus(userId),
                CvList = GetCvList(userId)
            };

            //if (overview.Stats.ProfileCount == 1 && overview.Stats.ExperienceCount >= 2 &&
            //    overview.Stats.CourseCount >= 2 && overview.Stats.SkillCount >= 2
            //    && overview.Stats.AwardCount >= 2 && overview.Stats.InterestCount >= 2
            //    && overview.Stats.ProjectCount >= 2)
            //{
            //    overview.IsReady = true;
            //}
            overview.IsReady = true;

            return overview;
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
                ProjectCount = query.ProjectCount
            };
            return stats;
        }

        private IEnumerable<SelectListItem> GetExperienceSelectList(string userId)
        {
            var experiences = _experienceService.GetAllExperienceByUserId(userId).ToList();
            if (!experiences.Any()) return null;


            var items = experiences.Select(x =>
                new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Title + " ," + x.Company
                });

            var selectListItems = items as IList<SelectListItem> ?? items.ToList();
            selectListItems.ToList().Add(new SelectListItem {Value = "0", Text = "Select Option"});

            return new SelectList(selectListItems, "Value", "Text");
        }

        private IEnumerable<SelectListItem> GetCourseSelectList(string userId)
        {
            var courses = _courseService.GetAllCourseByUserId(userId);
            if (courses == null) return null;
            var items = courses
                .Select(x =>
                    new SelectListItem
                    {
                        Value = x.Id.ToString(),
                        Text = x.Title + " ," + x.College
                    });
            var selectListItems = items as IList<SelectListItem> ?? items.ToList();
            selectListItems.ToList().Add(new SelectListItem {Value = "0", Text = "Select Option"});

            return new SelectList(selectListItems, "Value", "Text");
        }

        public CvTemplateDto GeCvTemplate(string cvId)
        {
            var template = _curriculumVitaeService.GetCvTemplate(cvId);
           
            var fix1 = template.Profile.PortfolioUrl;
            template.Profile.PortfolioUrl = "https://localhost:44300/GradFolio/Index/" + fix1;

            template.Profile.Email = User.Identity.GetUserName();
            
            return template;
        }
    }
}