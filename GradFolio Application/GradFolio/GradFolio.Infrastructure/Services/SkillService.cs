using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GradFolio.Core.DTO;
using GradFolio.Core.Repository;
using GradFolio.Core.Services;
using GradFolio.Infrastructure.Services.Helpers;

namespace GradFolio.Infrastructure.Services
{
    public class SkillService : DataAnnotationsValidator, ISkillService
    {
        private readonly ILoggingService _loggingService;
        private readonly ISkillRepository _skillRepository;
        private readonly IUserRepository _userRepository;

        public SkillService(
            ILoggingService loggingService,
            ISkillRepository skillRepository,
            IUserRepository userRepository)
        {
            _loggingService = loggingService;
            _skillRepository = skillRepository;
            _userRepository = userRepository;
        }

        public SkillDto GetSkillById(string userId, string skillId)
        {
            try
            {
                //Validate user
                if (_userRepository.IsAuthenticated(userId))
                {
                    //GetUserProfile
                    var skill = _skillRepository.GetSkillById(skillId);
                    if (skill != null)
                    {
                        //Success
                        return skill;
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

        public IEnumerable<SkillDto> GetAllSkillByUserId(string userId)
        {
            try
            {
                //Validate user
                if (_userRepository.IsAuthenticated(userId))
                {
                    //GetUserProfile
                    var skills = _skillRepository.GetAllSkillByUserId(userId);
                    if (skills != null)
                    {
                        //Success
                        return skills;
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

        public bool InsertSkill(string userId, SkillDto skill)
        {
            try
            {
                //Validate user
                if (_userRepository.IsAuthenticated(userId))
                {
                    //Validate Model
                    ICollection<ValidationResult> results;
                    if (IsValidModel(skill, out results))
                    {
                        //Call Repository
                        if (_skillRepository.InsertSkill(skill))
                        {
                            //Save
                            if (_skillRepository.Save())
                            {
                                //Success
                                return true;
                            }
                            _loggingService.Info("Failed To Save");
                        }
                        _loggingService.Info("UserRepository Failed Insert");
                    }
                    _loggingService.Info("Model Validation Failed: " + skill);
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

        public bool UpdateSkill(string userId, SkillDto skill)
        {
            try
            {
                //Validate user
                if (_userRepository.IsAuthenticated(userId))
                {
                    var record = _skillRepository.GetSkillById(skill.Id.ToString());
                    if (record != null)
                    {
                        //Validate Model
                        ICollection<ValidationResult> results;
                        if (IsValidModel(skill, out results))
                        {
                            skill.CreateDate = record.CreateDate;
                            if (ModelCompareChecker.Compare(skill, record))
                            {
                                return true;
                            }

                            record.Title = skill.Title;
                            record.Level = skill.Level;
                            record.Summary = skill.Summary;

                            return _skillRepository.UpdateSkill(record);
                        }
                        _loggingService.Info("Model Validation Failed: " + skill);
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

        public bool DeleteSkill(string userId, string skillId)
        {
            try
            {
                //Validate user
                if (_userRepository.IsAuthenticated(userId))
                {
                    //GetUserProfile
                    var skill = _skillRepository.GetSkillById(skillId);
                    if (skill != null)
                    {
                        //Success
                        var isDeleted = _skillRepository.DeleteSkill(skill.Id.ToString());
                        if (isDeleted)
                        {
                            return _skillRepository.Save();
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
