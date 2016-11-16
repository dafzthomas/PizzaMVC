using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PizzaWebSite.Startup))]
namespace PizzaWebSite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
