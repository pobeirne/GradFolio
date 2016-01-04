using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GradFolio.Core.DTO;
using GradFolio.Core.Repository;
using GradFolio.Core.Services;
using GradFolio.Infrastructure.Services.Helpers;

namespace GradFolio.Infrastructure.Services
{
    public class InterestService : DataAnnotationsValidator, IInterestService
    {
        private readonly ILoggingService _loggingService;
        private readonly IInterestRepository _interestRepository;
        private readonly IUserRepository _userRepository;

        public InterestService(
            ILoggingService loggingService, 
            IInterestRepository interestRepository, 
            IUserRepository userRepository)
        {
            _loggingService = loggingService;
            _interestRepository = interestRepository;
            _userRepository = userRepository;
        }

        public InterestDto GetInterestById(string userId, string interestId)
        {
            try
            {
                //Validate user
                if (_userRepository.IsAuthenticated(userId))
                {
                    //GetUserProfile
                    var interest = _interestRepository.GetInterestById(interestId);
                    if (interest != null)
                    {
                        //Success
                        return interest;
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

        public IEnumerable<InterestDto> GetAllInterestByUserId(string userId)
        {
            try
            {
                //Validate user
                if (_userRepository.IsAuthenticated(userId))
                {
                    //GetUserProfile
                    var interests = _interestRepository.GetAllInterestByUserId(userId);
                    if (interests != null)
                    {
                        //Success
                        return interests;
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

        public bool InsertInterest(string userId, InterestDto interest)
        {
            try
            {
                //Validate user
                if (_userRepository.IsAuthenticated(userId))
                {
                    //Validate Model
                    ICollection<ValidationResult> results;
                    if (IsValidModel(interest, out results))
                    {
                        //Call Repository
                        if (_interestRepository.InsertInterest(interest))
                        {
                            //Save
                            if (_interestRepository.Save())
                            {
                                //Success
                                return true;
                            }
                            _loggingService.Info("Failed To Save");
                        }
                        _loggingService.Info("UserRepository Failed Insert");
                    }
                    _loggingService.Info("Model Validation Failed: " + interest);
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

        public bool UpdateInterest(string userId, InterestDto interest)
        {
            try
            {
                //Validate user
                if (_userRepository.IsAuthenticated(userId))
                {
                    var record = _interestRepository.GetInterestById(interest.Id.ToString()); ;
                    if (record != null)
                    {
                        interest.CreateDate = record.CreateDate;
                        //Validate Model
                        ICollection<ValidationResult> results;
                        if (IsValidModel(interest, out results))
                        {
                            if (ModelCompareChecker.Compare(interest, record))
                            {
                                return true;
                            }

                            record.Title = interest.Title;
                            record.Summary = interest.Summary;

                            return _interestRepository.UpdateInterest(record);
                        }
                        _loggingService.Info("Model Validation Failed: " + interest);
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

        public bool DeleteInterest(string userId, string interestId)
        {
            try
            {
                //Validate user
                if (_userRepository.IsAuthenticated(userId))
                {
                    //GetUserProfile
                    var interest = _interestRepository.GetInterestById(interestId);
                    if (interest != null)
                    {
                        //Success
                        var isDeleted = _interestRepository.DeleteInterest(interest.Id.ToString());
                        if (isDeleted)
                        {
                            return _interestRepository.Save();
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
