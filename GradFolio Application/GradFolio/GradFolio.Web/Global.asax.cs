using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using NLog.Common;

namespace GradFolio.Web
{
    public class MvcApplication : HttpApplication
    {
      
        protected void Application_Start()
        {

            //NLogger error file 
            var nlogPath = Server.MapPath("~/App_Data/nlog-web-errors.log");
            InternalLogger.LogFile = nlogPath;


            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
         }
    }
}
