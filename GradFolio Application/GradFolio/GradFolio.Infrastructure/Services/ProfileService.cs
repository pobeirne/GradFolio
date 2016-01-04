using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GradFolio.Core.DTO;
using GradFolio.Core.Repository;
using GradFolio.Core.Services;
using GradFolio.Infrastructure.Services.Helpers;

namespace GradFolio.Infrastructure.Services
{
    public class ProfileService : DataAnnotationsValidator, IProfileService
    {
        private readonly ILoggingService _loggingService;
        private readonly IProfileRepository _profileRepository;
        private readonly IUserRepository _userRepository;

        public ProfileService(
            ILoggingService loggingService,
            IProfileRepository profileRepository,
            IUserRepository userRepository)
        {
            _loggingService = loggingService;
            _profileRepository = profileRepository;
            _userRepository = userRepository;
        }


        public ProfileDto GetUserProfile(string userId)
        {
            try
            {
                //Validate user
                if (_userRepository.IsAuthenticated(userId))
                {
                    //GetUserProfile
                    var profile = _profileRepository.GetUserProfileByUserId(userId);
                    if (profile != null)
                    {
                        //Success
                        return profile;
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


        public bool InsertProfile(string userId, ProfileDto profile)
        {
            try
            {
                //Validate user
                if (_userRepository.IsAuthenticated(userId))
                {
                    //Validate Model
                    ICollection<ValidationResult> results;
                    if (IsValidModel(profile, out results))
                    {
                        //Call Repository
                        if (_profileRepository.InsertProfile(profile))
                        {
                            //Save
                            if (_profileRepository.Save())
                            {
                                //Success
                                return true;
                            }
                            _loggingService.Info("Failed To Save");
                        }
                        _loggingService.Info("UserRepository Failed Insert");
                    }
                    _loggingService.Info("Model Validation Failed: " + profile);
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




        public bool UpdateProfile(string userId, ProfileDto profile)
        {
            try
            {
                //Validate user
                if (_userRepository.IsAuthenticated(userId))
                {
                    var existingProfile = GetUserProfile(userId);
                    if (existingProfile != null)
                    {
                        //Validate Model
                        ICollection<ValidationResult> results;
                        if (IsValidModel(profile, out results))
                        {
                            existingProfile.FirstName = profile.FirstName;
                            existingProfile.LastName = profile.LastName;
                            existingProfile.Title = profile.Title;
                            existingProfile.Summary = profile.Summary;
                            existingProfile.Location = profile.Location;
                            existingProfile.Mobile = profile.Mobile;
                            existingProfile.Phone = profile.Phone;
                            existingProfile.IsAvailable = profile.IsAvailable;
                            existingProfile.AvailableFromDate = profile.AvailableFromDate;
                            existingProfile.ImageUrl = profile.ImageUrl;
                            existingProfile.LinkedInUrl = profile.LinkedInUrl;

                            //Call Repository
                            if (_profileRepository.UpdateProfile(existingProfile))
                            {
                                return true;
                            }
                            _loggingService.Info("UserRepository Failed Update");
                        }
                        _loggingService.Info("Model Validation Failed: " + profile);
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
    }
}
