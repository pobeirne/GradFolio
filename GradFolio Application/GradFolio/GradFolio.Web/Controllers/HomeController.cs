using System.Web.Mvc;

namespace GradFolio.Web.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                RedirectToAction("Overview", "Portal");
            }
            return View();
        }
    }
}