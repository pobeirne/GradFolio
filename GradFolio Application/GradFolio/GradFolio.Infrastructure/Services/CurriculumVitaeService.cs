using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using GradFolio.Core.DTO;
using GradFolio.Core.Repository;
using GradFolio.Core.Services;
using GradFolio.Infrastructure.Services.Helpers;

namespace GradFolio.Infrastructure.Services
{
    public class CurriculumVitaeService : DataAnnotationsValidator, ICurriculumVitaeService
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
        private readonly ICurriculumVitaeRepository _curriculumVitaeRepository;

        public CurriculumVitaeService(ILoggingService loggingService, IUserRepository userRepository,
            IProfileRepository profileRepository, IExperienceRepository experienceRepository,
            ICourseRepository courseRepository, ISkillRepository skillRepository, IAwardRepository awardRepository,
            IInterestRepository interestRepository, IProjectRepository projectRepository,
            ICurriculumVitaeRepository curriculumVitaeRepository)
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
            _curriculumVitaeRepository = curriculumVitaeRepository;
        }

        public CurriculumVitaeDto GetCurriculumVitaeById(string userId, string cvId)
        {
            try
            {
                //Validate user
                if (_userRepository.IsAuthenticated(userId))
                {
                    //GetUserProfile
                    var cv = _curriculumVitaeRepository.GetCurriculumVitaeById(cvId);
                    if (cv != null)
                    {
                        //Success
                        return cv;
                    }
                    _loggingService.Info("Not profile for the user found: " + userId);
                }
                _loggingService.Info("UserId Authenticated Failed: " + userId);
            }
            catch (Exception ex)
            {
                //Error
                _loggingService.Error("An error has occurred", ex);
            }
            //Fail
            return null;
        }

        public IEnumerable<CurriculumVitaeDto> GetAllCurriculumVitaeByUserId(string userId)
        {
            try
            {
                //Validate user
                if (_userRepository.IsAuthenticated(userId))
                {
                    //GetUserProfile
                    var cvs = _curriculumVitaeRepository.GetAllCurriculumVitaeByUserId(userId);
                    if (cvs != null)
                    {
                        //Success
                        return cvs;
                    }
                    _loggingService.Info("Not profile for the user found: " + userId);
                }
                _loggingService.Info("UserId Authenticated Failed: " + userId);
            }
            catch (Exception ex)
            {
                //Error
                _loggingService.Error("An error has occurred", ex);
            }
            //Fail
            return null;
        }

        public bool InsertCurriculumVitae(string userId, CurriculumVitaeDto cv)
        {
            try
            {
                //Validate user
                if (_userRepository.IsAuthenticated(userId))
                {
                    //Validate Model
                    ICollection<ValidationResult> results;
                    if (IsValidModel(cv, out results))
                    {
                        //Call Repository
                        if (_curriculumVitaeRepository.InsertCurriculumVitae(cv))
                        {
                            //Save
                            if (_curriculumVitaeRepository.Save())
                            {
                                //Success
                                return true;
                            }
                            _loggingService.Info("Failed To Save");
                        }
                        _loggingService.Info("UserRepository Failed Insert");
                    }
                    _loggingService.Info("Model Validation Failed: " + cv);
                }
                _loggingService.Info("UserId Authenticated Failed: " + userId);
            }
            catch (Exception ex)
            {
                //Error
                _loggingService.Error("An error has occurred", ex);
            }
            //Fail
            return false;
        }

        public bool UpdateCurriculumVitae(string userId, CurriculumVitaeDto cv)
        {
            try
            {
                //Validate user
                if (_userRepository.IsAuthenticated(userId))
                {
                    var record = _curriculumVitaeRepository.GetCurriculumVitaeById(cv.Id.ToString());
                    if (record != null)
                    {
                        //validation
                        cv.CreateDate = record.CreateDate;
                        cv.RefNum = record.RefNum;
                        //Validate Model
                        ICollection<ValidationResult> results;
                        if (IsValidModel(cv, out results))
                        {
                            if (ModelCompareChecker.Compare(cv, record))
                            {
                                return true;
                            }

                            record.Name = cv.Name;
                            record.Experience1 = cv.Experience1;
                            record.Experience2 = cv.Experience2;
                            record.Course1 = cv.Course1;
                            record.Course2 = cv.Course2;

                            return _curriculumVitaeRepository.UpdateCurriculumVitae(record);
                        }
                        _loggingService.Info("Model Validation Failed: " + cv);
                    }
                }
                _loggingService.Info("UserId Authenticated Failed: " + userId);
            }
            catch (Exception ex)
            {
                //Error
                _loggingService.Error("An error has occurred", ex);
            }
            //Fail
            return false;
        }

        public bool DeleteCurriculumVitae(string userId, string cvId)
        {
            try
            {
                //Validate user
                if (_userRepository.IsAuthenticated(userId))
                {
                    //GetUserProfile
                    var cv = _curriculumVitaeRepository.GetCurriculumVitaeById(cvId);
                    if (cv != null)
                    {
                        //Success
                        var isDeleted = _curriculumVitaeRepository.DeleteCurriculumVitae(cv.Id.ToString());
                        if (isDeleted)
                        {
                            return _curriculumVitaeRepository.Save();
                        }
                    }
                    _loggingService.Info("Not profile for the user found: " + userId);
                }
                _loggingService.Info("UserId Authenticated Failed: " + userId);
            }
            catch (Exception ex)
            {
                //Error
                _loggingService.Error("An error has occurred", ex);
            }
            //Fail
            return false;
        }

        public bool GenerateCurriculumVitae(string userId, string filename)
        {
            var cv = new CurriculumVitaeDto
            {
                UserId = userId,
                Name = filename,
                Type = "BASIC"
            };


            //Experience
            if (_experienceRepository.GetAllExperienceByUserId(userId) != null)
            {
                var query =
                    _experienceRepository.GetAllExperienceByUserId(userId)
                        .OrderByDescending(m => m.CreateDate)
                        .Take(2)
                        .ToList();

                cv.Experience1 = query.ElementAt(0).Id.ToString();
                cv.Experience2 = query.ElementAt(1).Id.ToString();
            }

            //Course
            if (_courseRepository.GetAllCourseByUserId(userId) != null)
            {
                var query = _courseRepository.GetAllCourseByUserId(userId).OrderByDescending(m => m.CreateDate).ToList();
                cv.Course1 = query.ElementAt(0).Id.ToString();
                cv.Course2 = query.ElementAt(1).Id.ToString();
            }

            return InsertCurriculumVitae(userId, cv);
        }

        public CvTemplateDto GetCvTemplate(string cvId)
        {
            var cvConfig = _curriculumVitaeRepository.GetCurriculumVitaeById(cvId);
            var userId = cvConfig.UserId;
            var template = new CvTemplateDto();

            //Profile
            if (_profileRepository.GetUserProfileByUserId(userId) != null)
            {
                template.Profile = _profileRepository.GetUserProfileByUserId(userId);
            }

            //Experience
            if (_experienceRepository.GetAllExperienceByUserId(userId) != null)
            {
                template.Experiences = _experienceRepository.GetAllExperienceByUserId(userId)
                    .Where(x => x.Id.ToString() == cvConfig.Experience1
                                || x.Id.ToString() == cvConfig.Experience2);
            }

            //Course
            if (_courseRepository.GetAllCourseByUserId(userId) != null)
            {
                template.Courses = _courseRepository.GetAllCourseByUserId(userId)
                    .Where(x => x.Id.ToString() == cvConfig.Course1
                                || x.Id.ToString() == cvConfig.Course2);
            }

            //Skill
            if (_skillRepository.GetAllSkillByUserId(userId) != null)
            {
                template.Skills = _skillRepository.GetAllSkillByUserId(userId).Take(10);
            }


            //Award
            if (_awardRepository.GetAllAwardByUserId(userId) != null)
            {
                template.Awards = _awardRepository.GetAllAwardByUserId(userId).Take(5);
            }

            //Interest
            if (_interestRepository.GetAllInterestByUserId(userId) != null)
            {
                template.Interests = _interestRepository.GetAllInterestByUserId(userId).Take(5);
            }

            //Project
            if (_projectRepository.GetAllProjectByUserId(userId) != null)
            {
                template.Projects = _projectRepository.GetAllProjectByUserId(userId).Take(5);
            }


            //Portfolio

            //if (_portfolioRepository.GetAllProjectByUserId(userId) != null)
            //{
            //    template.Projects = _projectRepository.GetAllProjectByUserId(userId).Take(5);
            //}

            //overview.PortfolioCount = 0;


            return template;
        }
    }
}
