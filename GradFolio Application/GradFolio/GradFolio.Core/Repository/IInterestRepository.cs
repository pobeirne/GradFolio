using System.Collections.Generic;
using GradFolio.Core.DTO;

namespace GradFolio.Core.Repository
{
    public interface IInterestRepository
    {
        InterestDto GetInterestById(string interestId);
        IEnumerable<InterestDto> GetAllInterestByUserId(string userId);
        bool InsertInterest(InterestDto interest);
        bool UpdateInterest(InterestDto interest);
        bool DeleteInterest(string interestId);
        bool Save();
    }
}
