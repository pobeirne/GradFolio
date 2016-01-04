using GradFolio.Core.DTO;

namespace GradFolio.Core.Services
{
    public interface IPortfolioService
    {
        //PortfolioViewModel GetPortfolioById(string guid);
        //PortfolioSettingViewModel GetPortfolioSettingsByType(string userId, string type);
        //bool UpdatePortfolioSetting(string userId, PostedSetting ids, string type);
        //bool DeletePortfolio(string userId, int portfolioId);



        PortfolioDto GetPortfolioByUserId(string userId);
        GradFolioDto GetPortfolioByRefNum(long refId);
        bool InsertPortfolio(PortfolioDto portfolio, string userId);
        bool UpdatePortfolio(PortfolioDto portfolio, string userId);
        bool DeletePortfolio(string userId, string portfolioId);

    }
}
