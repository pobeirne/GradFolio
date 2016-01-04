using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GradFolio.Web.Startup))]

namespace GradFolio.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
