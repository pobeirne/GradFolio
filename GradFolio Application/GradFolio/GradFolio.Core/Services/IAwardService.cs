using GradFolio.Core.ViewModels;
using System.Collections.Generic;
using GradFolio.Core.DTO;

namespace GradFolio.Core.Services
{
    public interface IAwardService
    {
        AwardDto GetAwardById(string userId, string awardId);
        IEnumerable<AwardDto> GetAllAwardByUserId(string userId);
        bool InsertAward(string userId, AwardDto award);
        bool UpdateAward(string userId, AwardDto award);
        bool DeleteAward(string userId, string awardId);

    }
}
