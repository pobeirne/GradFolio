using System.Collections.Generic;
using GradFolio.Core.DTO;
using GradFolio.Core.ViewModels;

namespace GradFolio.Core.Services
{
    public interface IInterestService
    {
        InterestDto GetInterestById(string userId, string interestId);
        IEnumerable<InterestDto> GetAllInterestByUserId(string userId);
        bool InsertInterest(string userId, InterestDto interest);
        bool UpdateInterest(string userId, InterestDto interest);
        bool DeleteInterest(string userId, string interestId);
    }
}
