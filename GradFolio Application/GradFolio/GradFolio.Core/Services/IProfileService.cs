using GradFolio.Core.DTO;

namespace GradFolio.Core.Services
{
    public interface IProfileService
    {
        ProfileDto GetUserProfile(string userId);
        bool UpdateProfile(string userId, ProfileDto profile);


        bool InsertProfile(string userId, ProfileDto insert);
    }
}
