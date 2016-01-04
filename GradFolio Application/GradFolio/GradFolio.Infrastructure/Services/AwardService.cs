using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GradFolio.Core.DTO;
using GradFolio.Core.Repository;
using GradFolio.Core.Services;
using GradFolio.Infrastructure.Services.Helpers;

namespace GradFolio.Infrastructure.Services
{
    public class AwardService : DataAnnotationsValidator, IAwardService
    {
        private readonly ILoggingService _loggingService;
        private readonly IAwardRepository _awardRepository;
        private readonly IUserRepository _userRepository;

        public AwardService(
            ILoggingService loggingService,
            IAwardRepository awardRepository,
            IUserRepository userRepository)
        {
            _loggingService = loggingService;
            _awardRepository = awardRepository;
            _userRepository = userRepository;
        }

        public AwardDto GetAwardById(string userId, string awardId)
        {
            try
            {
                //Validate user
                if (_userRepository.IsAuthenticated(userId))
                {
                    //GetUserProfile
                    var award = _awardRepository.GetAwardById(awardId);
                    if (award != null)
                    {
                        //Success
                        return award;
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

        public IEnumerable<AwardDto> GetAllAwardByUserId(string userId)
        {
            try
            {
                //Validate user
                if (_userRepository.IsAuthenticated(userId))
                {
                    //GetUserProfile
                    var awards = _awardRepository.GetAllAwardByUserId(userId);
                    if (awards != null)
                    {
                        //Success
                        return awards;
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

        public bool InsertAward(string userId, AwardDto award)
        {
            try
            {
                //Validate user
                if (_userRepository.IsAuthenticated(userId))
                {
                    //Validate Model
                    ICollection<ValidationResult> results;
                    if (IsValidModel(award, out results))
                    {
                        //Call Repository
                        if (_awardRepository.InsertAward(award))
                        {
                            //Save
                            if (_awardRepository.Save())
                            {
                                //Success
                                return true;
                            }
                            _loggingService.Info("Failed To Save");
                        }
                        _loggingService.Info("UserRepository Failed Insert");
                    }
                    _loggingService.Info("Model Validation Failed: " + award);
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

        public bool UpdateAward(string userId, AwardDto award)
        {
            try
            {
                //Validate user
                if (_userRepository.IsAuthenticated(userId))
                {
                    var record = _awardRepository.GetAwardById(award.Id.ToString());
                    if (record != null)
                    {
                        //Validate Model
                        ICollection<ValidationResult> results;
                        if (IsValidModel(award, out results))
                        {
                            if (ModelCompareChecker.Compare(award, record))
                            {
                                return true;
                            }

                            record.Title = award.Title;
                            record.Level = award.Level;
                            record.IssuedBy = award.IssuedBy;
                            record.IssuedDate = award.IssuedDate;

                            return _awardRepository.UpdateAward(record);
                        }
                        _loggingService.Info("Model Validation Failed: " + award);
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

        public bool DeleteAward(string userId, string awardId)
        {
            try
            {
                //Validate user
                if (_userRepository.IsAuthenticated(userId))
                {
                    //GetUserProfile
                    var award = _awardRepository.GetAwardById(awardId);
                    if (award != null)
                    {
                        //Success
                        var isDeleted = _awardRepository.DeleteAward(award.Id.ToString());
                        if (isDeleted)
                        {
                            return _awardRepository.Save();
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
