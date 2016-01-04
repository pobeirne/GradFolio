using System;
using System.Data.Entity.Migrations;
using System.Linq;
using GradFolio.Core.DTO;
using GradFolio.Core.Model;
using GradFolio.Core.Repository;
using GradFolio.Infrastructure.Data;

namespace GradFolio.Infrastructure.Repository
{
    public class PorfolioRepository : IPortfolioRepository
    {
        private readonly GradFolioContext _context = new GradFolioContext();

        public PortfolioDto GetPortfolioByUserId(string userId)
        {
            var portfolio = _context.Portfolios.FirstOrDefault(x => x.UserId.ToString() == userId);
            if (portfolio == null) return null;
            return new PortfolioDto()
            {
                Id = portfolio.Id,
                UserId = portfolio.UserId,
                Type = portfolio.Type,
                RefNum = portfolio.RefNum,
                CreateDate = portfolio.CreateDate
            };
        }

        public PortfolioDto GetPortfolioByRefNum(long refId)
        {
            var portfolio = _context.Portfolios.FirstOrDefault(x => x.RefNum == refId);
            if (portfolio == null) return null;
            return new PortfolioDto()
            {
                Id = portfolio.Id,
                UserId = portfolio.UserId,
                Type = portfolio.Type,
                RefNum = portfolio.RefNum,
                CreateDate = portfolio.CreateDate
            };
        }

        public bool InsertPortfolio(PortfolioDto portfolio)
        {
            return _context.Portfolios.Add(new Portfolio
            {
                UserId = portfolio.UserId,
                Type = portfolio.Type
            }) != null;
        }

        public bool UpdatePortfolio(PortfolioDto portfolio)
        {
            var record = _context.Portfolios.FirstOrDefault(x => x.Id == portfolio.Id);
            if (record == null) return false;
            record.Type = portfolio.Type;
            _context.Portfolios.AddOrUpdate(record);
            return _context.SaveChanges() == 1;
        }

        public bool DeletePortfolio(string portfolioId)
        {
            var record = _context.Portfolios.FirstOrDefault(x => x.Id.ToString() == portfolioId);
            return _context.Portfolios.Remove(record) != null;
        }

        public bool Save()
        {
            try
            {
                return _context.SaveChanges() == 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
