using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SalesForceWebApp.Startup))]
namespace SalesForceWebApp
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
