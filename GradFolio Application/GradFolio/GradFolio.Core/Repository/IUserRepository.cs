namespace GradFolio.Core.Repository
{
    public interface IUserRepository
    {
        bool IsAuthenticated(string userId);
    }
}
