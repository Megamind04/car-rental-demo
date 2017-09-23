using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CarRentalDemo.Web.Startup))]
namespace CarRentalDemo.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
