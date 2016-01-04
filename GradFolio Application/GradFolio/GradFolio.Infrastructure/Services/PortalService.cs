using System.Linq;
using GradFolio.Core.DTO;
using GradFolio.Core.Repository;
using GradFolio.Core.Services;

namespace GradFolio.Infrastructure.Services
{
    public class PortalService : IPortalService
    {
        private readonly ILoggingService _loggingService;
        private readonly IUserRepository _userRepository;
        private readonly IProfileRepository _profileRepository;
        private readonly IExperienceRepository _experienceRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly ISkillRepository _skillRepository;
        private readonly IAwardRepository _awardRepository;
        private readonly IInterestRepository _interestRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly ICurriculumVitaeService _curriculumVitaeService;
        private readonly IPortfolioService _portfolioService; 

        public PortalService(ILoggingService loggingService, IUserRepository userRepository,
            IProfileRepository profileRepository, IExperienceRepository experienceRepository,
            ICourseRepository courseRepository, ISkillRepository skillRepository, IAwardRepository awardRepository,
            IInterestRepository interestRepository, IProjectRepository projectRepository, ICurriculumVitaeService curriculumVitaeService, IPortfolioService portfolioService)
        {
            _loggingService = loggingService;
            _userRepository = userRepository;
            _profileRepository = profileRepository;
            _experienceRepository = experienceRepository;
            _courseRepository = courseRepository;
            _skillRepository = skillRepository;
            _awardRepository = awardRepository;
            _interestRepository = interestRepository;
            _projectRepository = projectRepository;
            _curriculumVitaeService = curriculumVitaeService;
            _portfolioService = portfolioService;
        }


        public PortalOverviewDto GetPortalOverview(string userId)
        {
            var overview = new PortalOverviewDto();

            //Profile
            if (_profileRepository.GetUserProfileByUserId(userId) != null)
            {
                overview.Profile = _profileRepository.GetUserProfileByUserId(userId);
                overview.ProfileCount = 1;
            }
            else
            {
                overview.ProfileCount = 0;
            }

            //Experience
            if (_experienceRepository.GetAllExperienceByUserId(userId) != null)
            {
                var query = _experienceRepository.GetAllExperienceByUserId(userId).ToList();
                overview.ExperienceCount = query.Count();
                overview.LatestExperience = query.OrderByDescending(m => m.CreateDate).FirstOrDefault();
            }
            else
            {
                overview.ExperienceCount = 0;
            }


            //Course
            if (_courseRepository.GetAllCourseByUserId(userId) != null)
            {
                var query = _courseRepository.GetAllCourseByUserId(userId).ToList();
                overview.CourseCount = query.Count();
                overview.LatestCourse = query.OrderByDescending(m => m.CreateDate).FirstOrDefault();
            }
            else
            {
                overview.CourseCount = 0;
            }

            //Skill
            if (_skillRepository.GetAllSkillByUserId(userId) != null)
            {
                var query = _skillRepository.GetAllSkillByUserId(userId).ToList();
                overview.SkillCount = query.Count();
                overview.LatestSkill = query.OrderByDescending(m => m.CreateDate).FirstOrDefault();
            }
            else
            {
                overview.SkillCount = 0;
            }

            //Award
            if (_awardRepository.GetAllAwardByUserId(userId) != null)
            {
                var query = _awardRepository.GetAllAwardByUserId(userId).ToList();
                overview.AwardCount = query.Count();
                overview.LatestAward = query.OrderByDescending(m => m.CreateDate).FirstOrDefault();
            }
            else
            {
                overview.AwardCount = 0;
            }


            //Interest
            if (_interestRepository.GetAllInterestByUserId(userId) != null)
            {
                var query = _interestRepository.GetAllInterestByUserId(userId).ToList();
                overview.InterestCount = query.Count();
                overview.LatestInterest = query.OrderByDescending(m => m.CreateDate).FirstOrDefault();
            }
            else
            {
                overview.InterestCount = 0;
            }


            //Project
            if (_projectRepository.GetAllProjectByUserId(userId) != null)
            {
                var query = _projectRepository.GetAllProjectByUserId(userId).ToList();
                overview.ProjectCount = query.Count();
                overview.LatestProject = query.OrderByDescending(m => m.CreateDate).FirstOrDefault();
            }
            else
            {
                overview.ProjectCount = 0;
            }

            //CV
            if (_curriculumVitaeService.GetAllCurriculumVitaeByUserId(userId) != null)
            {
                var query = _curriculumVitaeService.GetAllCurriculumVitaeByUserId(userId).ToList();
                overview.CvCount = query.Count();
                overview.LatestCv = query.OrderByDescending(m => m.CreateDate).FirstOrDefault();
            }
            else
            {
                overview.CvCount = 0;
            }

            //PortFolio
            if (_portfolioService.GetPortfolioByUserId(userId) != null)
            {
                overview.PortfolioCount = 1;
                overview.LatestPortfolio = _portfolioService.GetPortfolioByUserId(userId);
            }
            else
            {
                overview.PortfolioCount = 0;
            }
            
            
            return overview;
        }
    }
}
