using GradFolio.Core.DTO;

namespace GradFolio.Core.Repository
{
    public interface IPortfolioRepository
    {
        PortfolioDto GetPortfolioByUserId(string userId);
        PortfolioDto GetPortfolioByRefNum(long refId);
        bool InsertPortfolio(PortfolioDto portfolio);
        bool UpdatePortfolio(PortfolioDto portfolio);
        bool DeletePortfolio(string portfolioId);
        bool Save();
    }
}
