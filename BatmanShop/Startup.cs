using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BatmanShop.Startup))]
namespace BatmanShop
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
