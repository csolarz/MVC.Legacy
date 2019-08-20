using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVC.Legacy.Startup))]
namespace MVC.Legacy
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
