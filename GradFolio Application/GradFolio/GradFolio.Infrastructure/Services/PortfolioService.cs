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
    public class PortfolioService : DataAnnotationsValidator, IPortfolioService
    {
        private readonly ILoggingService _loggingService;
        private readonly IUserRepository _userRepository;
        private readonly IPortfolioRepository _portfolioRepository;

        private readonly IProfileRepository _profileRepository;
        private readonly IExperienceRepository _experienceRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly ISkillRepository _skillRepository;
        private readonly IAwardRepository _awardRepository;
        private readonly IInterestRepository _interestRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly ICurriculumVitaeRepository _curriculumVitaeRepository;

        public PortfolioService(
            ILoggingService loggingService,
            IUserRepository userRepository,
            IPortfolioRepository portfolioRepository, IProfileRepository profileRepository,
            IExperienceRepository experienceRepository, ICourseRepository courseRepository,
            ISkillRepository skillRepository, IAwardRepository awardRepository, IInterestRepository interestRepository,
            IProjectRepository projectRepository, ICurriculumVitaeRepository curriculumVitaeRepository)
        {
            _loggingService = loggingService;
            _userRepository = userRepository;
            _portfolioRepository = portfolioRepository;
            _profileRepository = profileRepository;
            _experienceRepository = experienceRepository;
            _courseRepository = courseRepository;
            _skillRepository = skillRepository;
            _awardRepository = awardRepository;
            _interestRepository = interestRepository;
            _projectRepository = projectRepository;
            _curriculumVitaeRepository = curriculumVitaeRepository;
        }


        public PortfolioDto GetPortfolioByUserId(string userId)
        {
            try
            {
                //Validate user
                if (_userRepository.IsAuthenticated(userId))
                {
                    //GetUserProfile
                    var portfolio = _portfolioRepository.GetPortfolioByUserId(userId);
                    if (portfolio != null)
                    {
                        //Success
                        return portfolio;
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

        public bool InsertPortfolio(PortfolioDto portfolio, string userId)
        {
            try
            {
                //Validate user
                if (_userRepository.IsAuthenticated(userId))
                {
                    //Validate Model
                    ICollection<ValidationResult> results;
                    if (IsValidModel(portfolio, out results))
                    {
                        //Call Repository
                        if (_portfolioRepository.InsertPortfolio(portfolio))
                        {
                            //Save
                            if (_portfolioRepository.Save())
                            {
                                var profile = _profileRepository.GetUserProfileByUserId(userId);
                                var record = _portfolioRepository.GetPortfolioByUserId(userId);
                                if (profile != null)
                                {
                                    profile.PortfolioUrl = record.RefNum.ToString();
                                    var isUpdated = _profileRepository.UpdateProfile(profile);
                                    if (isUpdated)
                                    {
                                        //Success
                                        return true;
                                    }
                                }
                            }
                            _loggingService.Info("Failed To Save");
                        }
                        _loggingService.Info("UserRepository Failed Insert");
                    }
                    _loggingService.Info("Model Validation Failed: " + portfolio);
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

        public bool UpdatePortfolio(PortfolioDto portfolio, string userId)
        {
            try
            {
                //Validate user
                if (_userRepository.IsAuthenticated(userId))
                {
                    var record = _portfolioRepository.GetPortfolioByUserId(userId);
                    if (record != null)
                    {
                        portfolio.CreateDate = record.CreateDate;
                        //Validate Model
                        ICollection<ValidationResult> results;
                        if (IsValidModel(portfolio, out results))
                        {
                            if (ModelCompareChecker.Compare(portfolio, record))
                            {
                                return true;
                            }

                            record.Type = portfolio.Type;

                            return _portfolioRepository.UpdatePortfolio(record);
                        }
                        _loggingService.Info("Model Validation Failed: " + portfolio);
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

        public bool DeletePortfolio(string userId, string portfolioId)
        {
            try
            {
                //Validate user
                if (_userRepository.IsAuthenticated(userId))
                {
                    //GetUserProfile
                    var portfolio = _portfolioRepository.GetPortfolioByUserId(userId);
                    if (portfolio != null)
                    {
                        //Success
                        var isDeleted = _portfolioRepository.DeletePortfolio(portfolio.Id.ToString());
                        if (isDeleted)
                        {
                            if (_portfolioRepository.Save())
                            {
                                var profile = _profileRepository.GetUserProfileByUserId(userId);
                                if (profile != null)
                                {
                                    profile.PortfolioUrl = "";
                                    var isUpdated = _profileRepository.UpdateProfile(profile);
                                    if (isUpdated)
                                    {
                                        //Success
                                        return true;
                                    }
                                }
                            }
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


        public GradFolioDto GetPortfolioByRefNum(long refId)
        {
            try
            {
                var portfolio = _portfolioRepository.GetPortfolioByRefNum(refId);
                var userId = portfolio.UserId;


                var template = new GradFolioDto();


                //Profile
                if (_profileRepository.GetUserProfileByUserId(userId) != null)
                {
                    template.Profile = _profileRepository.GetUserProfileByUserId(userId);
                }

                //Experience
                if (_experienceRepository.GetAllExperienceByUserId(userId) != null)
                {
                    template.Experiences = _experienceRepository.GetAllExperienceByUserId(userId);
                }

                //Course
                if (_courseRepository.GetAllCourseByUserId(userId) != null)
                {
                    template.Courses = _courseRepository.GetAllCourseByUserId(userId);
                }

                //Skill
                if (_skillRepository.GetAllSkillByUserId(userId) != null)
                {
                    template.Skills = _skillRepository.GetAllSkillByUserId(userId);
                }


                //Award
                if (_awardRepository.GetAllAwardByUserId(userId) != null)
                {
                    template.Awards = _awardRepository.GetAllAwardByUserId(userId);
                }

                //Interest
                if (_interestRepository.GetAllInterestByUserId(userId) != null)
                {
                    template.Interests = _interestRepository.GetAllInterestByUserId(userId);
                }

                //Project
                if (_projectRepository.GetAllProjectByUserId(userId) != null)
                {
                    template.Projects = _projectRepository.GetAllProjectByUserId(userId);
                }

                //CV
                if (_curriculumVitaeRepository.GetAllCurriculumVitaeByUserId(userId) != null)
                {
                    template.CurriculumVitae = _curriculumVitaeRepository.GetAllCurriculumVitaeByUserId(userId).First();
                }

                return template;
            }
            catch (Exception ex)
            {
                //Error
                _loggingService.Error("An error has occurred", ex);
            }
            //Fail
            return null;
        }
    }
}
