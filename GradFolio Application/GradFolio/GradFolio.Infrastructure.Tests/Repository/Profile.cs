using System;
using System.Diagnostics;
using GradFolio.Core.DTO;
using GradFolio.Infrastructure.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GradFolio.Infrastructure.Tests.Repository
{
    [TestClass]
    public class Profile
    {
        private ProfileRepository _profileRepository;
        public string UserId;

        [TestInitialize]
        public void Initialise()
        {
            _profileRepository = new ProfileRepository();
            UserId = "d5390018-e4ba-4ae4-add2-aaaa634f1a92";
        }

        [TestMethod]
        public void GetUserProfileByUserId()
        {
            var profile = _profileRepository.GetUserProfileByUserId(UserId);
            if (profile != null)
            {
                Debug.WriteLine("Time {0}\n", DateTime.Now);

                Trace.TraceInformation("\nProfile ID: {0}", profile.Id);

                Debug.WriteLine("\nTime {0}", DateTime.Now);
            }
            else
            {
                Trace.WriteLine("Profile not found!!!");
            }
        }

        [TestMethod]
        public void InsertProfile()
        {
            var profile = _profileRepository.GetUserProfileByUserId(UserId);

            if (profile != null)
            {
                Trace.WriteLine("Profile Already Exists!!!");
            }
            else
            {
                var isInserted = _profileRepository.InsertProfile(new ProfileDto()
                {
                    UserId = UserId,
                    FirstName = "Paul",
                    LastName = "Obeirne",
                    Title = "Software developer",
                    Summary =
                        "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vestibulum non hendrerit enim. Donec dolor felis, placerat in sodales accumsan, fringilla ut dolor. Mauris consequat malesuada neque, ut ultrices quam egestas bibendum. Duis metus nisi, pulvinar ac dolor id, laoreet tristique nunc. Nulla sed quam ut lorem molestie sagittis eget sit amet mauris. Pellentesque malesuada sit amet velit et maximus. Suspendisse potenti. Nam ut dui eleifend, suscipit lorem ac, tristique dui. Morbi ut ligula eget metus ullamcorper fermentum.Nam ut dui eleifend, suscipit lorem ac, tristique dui.",
                    Location = "Dublin",
                    Phone = "0858338278",
                    Mobile = "0858338278",
                    IsAvailable = true,
                    AvailableFromDate = DateTime.Now,
                    ImageUrl =
                        "https://media.licdn.com/mpr/mpr/shrinknp_400_400/AAEAAQAAAAAAAANKAAAAJGIyMDQzZTA1LWY4ZmItNDIyYi05NWU0LTliOTk2M2Y0NWY3NA.jpg",
                    PortfolioUrl = "not implemented",
                    LinkedInUrl = "URL"
                });

                if (isInserted)
                {
                    var isSaved = _profileRepository.Save();
                    Trace.WriteLine(isSaved ? "Profile Added!!!" : "Profile Not Added!!! Save Method Error");
                }
                else
                {
                    Trace.WriteLine("Profile Not Added!!! Insert Method Error");
                }
            }
        }

        [TestMethod]
        public void UpdateProfile()
        {
            var profile = _profileRepository.GetUserProfileByUserId(UserId);

            if (profile != null)
            {
                profile.FirstName = "TestFirstNameUpdated";

                var isUpdated = _profileRepository.UpdateProfile(profile);

                Trace.WriteLine(isUpdated ? "Profile Update!!!" : "Update Method Error!!!");
            }
            else
            {
                Trace.WriteLine("Profile Doesnt Exist!!!");
            }
        }


        [TestMethod]
        public void DeleteProfile()
        {
            var profile = _profileRepository.GetUserProfileByUserId(UserId);
            if (profile != null)
            {
                var isDeleted = _profileRepository.DeleteProfile(profile.UserId);

                if (isDeleted)
                {
                    //Will actually delete records
                    //var isSaved = _profileRepository.Save();
                    //Trace.WriteLine(isSaved ? "Profile Deleted!!!" : "Save Method Error!!!");

                    Trace.WriteLine("Profile Deleted!!!");
                }
                else
                {
                    Trace.WriteLine("Profile Not Deleted!!! Delete Method Error");
                }
            }
            else
            {
                Trace.WriteLine("Profile not found!!!");
            }
        }
    }
}
