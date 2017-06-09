using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GoatWebShop.Startup))]
namespace GoatWebShop
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
