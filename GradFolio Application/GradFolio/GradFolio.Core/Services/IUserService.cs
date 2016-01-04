namespace GradFolio.Core.Services
{
    public interface IUserService
    {
        bool IsAuthenticated(string userId);
    }
}
