using GradFolio.Core.DTO;
using GradFolio.Core.ViewModels;

namespace GradFolio.Core.Services
{
    public interface IPortalService
    {
        PortalOverviewDto GetPortalOverview(string userId);
        //PortalOverviewViewModel GetOverview(string userId);
    }
}
