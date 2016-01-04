using System;
using GradFolio.Core.Repository;
using GradFolio.Core.Services;

namespace GradFolio.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly ILoggingService _loggingService;
        private readonly IUserRepository _userRepository;

        public UserService(ILoggingService loggingService, IUserRepository userRepository)
        {
            _loggingService = loggingService;
            _userRepository = userRepository;
        }

        public bool IsAuthenticated(string userId)
        {
            try
            {
                return _userRepository.IsAuthenticated(userId);
            }
            catch (Exception ex)
            {
                _loggingService.Error("An error has occurred", ex);
            }
            return false;
        }
    }
}
