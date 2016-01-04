using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GradFolio.Core.DTO;
using GradFolio.Core.Repository;
using GradFolio.Core.Services;
using GradFolio.Infrastructure.Services.Helpers;

namespace GradFolio.Infrastructure.Services
{
    public class ExperienceService : DataAnnotationsValidator, IExperienceService
    {
        private readonly ILoggingService _loggingService;
        private readonly IExperienceRepository _experienceRepository;
        private readonly IUserRepository _userRepository;

        public ExperienceService(
            ILoggingService loggingService,
            IExperienceRepository experienceRepository,
            IUserRepository userRepository)
        {
            _loggingService = loggingService;
            _experienceRepository = experienceRepository;
            _userRepository = userRepository;
        }


        public ExperienceDto GetExperienceById(string userId, string experienceId)
        {
            try
            {
                //Validate user
                if (_userRepository.IsAuthenticated(userId))
                {
                    //GetUserProfile
                    var experience = _experienceRepository.GetExperienceById(experienceId);
                    if (experience != null)
                    {
                        //Success
                        return experience;
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

        public IEnumerable<ExperienceDto> GetAllExperienceByUserId(string userId)
        {
            try
            {
                //Validate user
                if (_userRepository.IsAuthenticated(userId))
                {
                    //GetUserProfile
                    var experiences = _experienceRepository.GetAllExperienceByUserId(userId);
                    if (experiences != null)
                    {
                        //Success
                        return experiences;
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

        public bool InsertExperience(string userId, ExperienceDto experience)
        {
            try
            {
                //Validate user
                if (_userRepository.IsAuthenticated(userId))
                {
                    //Validate Model
                    ICollection<ValidationResult> results;
                    if (IsValidModel(experience, out results))
                    {
                        //Call Repository
                        if (_experienceRepository.InsertExperience(experience))
                        {
                            //Save
                            if (_experienceRepository.Save())
                            {
                                //Success
                                return true;
                            }
                            _loggingService.Info("Failed To Save");
                        }
                        _loggingService.Info("UserRepository Failed Insert");
                    }
                    _loggingService.Info("Model Validation Failed: " + experience);
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

        public bool UpdateExperience(string userId, ExperienceDto experience)
        {
            try
            {
                //Validate user
                if (_userRepository.IsAuthenticated(userId))
                {
                    var record = _experienceRepository.GetExperienceById(experience.Id.ToString());
                    if (record != null)
                    {
                        //Validate Model
                        ICollection<ValidationResult> results;
                        if (IsValidModel(experience, out results))
                        {
                            if (ModelCompareChecker.Compare(experience, record))
                            {
                                return true;
                            }
                            record.Title = experience.Title;
                            record.Company = experience.Company;
                            record.Summary = experience.Summary;
                            record.Location = experience.Location;
                            record.StartDate = experience.StartDate;
                            record.EndDate = experience.EndDate;
                            record.IsCurrent = experience.IsCurrent;

                            //Call Repository
                            if (_experienceRepository.UpdateExperience(record))
                            {
                                return true;
                            }
                            _loggingService.Info("UserRepository Failed Update");
                        }
                        _loggingService.Info("Model Validation Failed: " + experience);
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

        public bool DeleteExperience(string userId, string experienceId)
        {
            try
            {
                //Validate user
                if (_userRepository.IsAuthenticated(userId))
                {
                    //GetUserProfile
                    var experience = _experienceRepository.GetExperienceById(experienceId);
                    if (experience != null)
                    {

                        //Success
                        var isDeleted = _experienceRepository.DeleteExperience(experience.Id.ToString());
                        if (isDeleted)
                        {
                            return _experienceRepository.Save();
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
    }
}
