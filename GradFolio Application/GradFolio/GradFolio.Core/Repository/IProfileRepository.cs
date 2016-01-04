using GradFolio.Core.DTO;

namespace GradFolio.Core.Repository
{
    public interface IProfileRepository
    {
        ProfileDto GetUserProfileByUserId(string userId);
        bool InsertProfile(ProfileDto profile);
        bool UpdateProfile(ProfileDto profile);
        bool DeleteProfile(string userId);
        bool Save();
    }
}
