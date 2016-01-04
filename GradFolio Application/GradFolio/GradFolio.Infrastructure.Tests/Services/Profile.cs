using System;
using System.Diagnostics;
using GradFolio.Core.Services;
using GradFolio.Infrastructure.Logging.NLog;
using GradFolio.Infrastructure.Repository;
using GradFolio.Infrastructure.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GradFolio.Infrastructure.Tests.Services
{
    [TestClass]
    public class Profile
    {
        //private IUserProfileService _profileService;

        //[TestInitialize]
        //public void Initialise()
        //{
        //    _profileService = new UserProfileService(
        //        new LoggingService(),
        //        new UserProfileRepository(),
        //        new UserRepository() );
        //}


        //[TestMethod]
        //public void Get_User_Profile_By_UserId()
        //{
        //    var profile = _profileService.GetUserProfileByUserId("123345678");
        //    if (profile != null)
        //    {
        //        Debug.WriteLine("Time {0}", DateTime.Now);
        //        Trace.TraceInformation("Profile ID: {0}", profile.Id);
        //        Debug.WriteLine("Time {0}", DateTime.Now);
        //    }
        //    else
        //    {
        //        Trace.WriteLine("Profile not found!!!");
        //    }
        //}
    }
}
