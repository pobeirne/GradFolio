using System.Collections.Generic;
using GradFolio.Core.DTO;

namespace GradFolio.Core.Repository
{
    public interface IAwardRepository
    {
        AwardDto GetAwardById(string awardId);
        IEnumerable<AwardDto> GetAllAwardByUserId(string userId);
        bool InsertAward(AwardDto award);
        bool UpdateAward(AwardDto award);
        bool DeleteAward(string awardId);
        bool Save();
    }
}
