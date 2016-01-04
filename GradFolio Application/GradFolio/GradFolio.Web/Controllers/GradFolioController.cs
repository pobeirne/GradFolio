using System.Web.Mvc;
using GradFolio.Core.Services;

namespace GradFolio.Web.Controllers
{
    public class GradFolioController : Controller
    {

        private readonly ILoggingService _loggingService;
        private readonly IPortfolioService _portfolioService;

        public GradFolioController(
            ILoggingService loggingService, 
            IPortfolioService portfolioService)
        {
            _loggingService = loggingService;
            _portfolioService = portfolioService;
        }


        // GET: GradFolio
        public ActionResult Index(long id)
        {
            var model = _portfolioService.GetPortfolioByRefNum(id);
            return View(model);
        }
    }
}