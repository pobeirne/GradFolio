using GradFolio.Core.Repository;
using GradFolio.Infrastructure.Data;

namespace GradFolio.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly GradFolioContext _context;

        public UserRepository()
        {
            _context = new GradFolioContext();
        }

        public bool IsAuthenticated(string userId)
        {
            return _context.AspNetUsers.Find(userId) != null;
        }
    }
}
