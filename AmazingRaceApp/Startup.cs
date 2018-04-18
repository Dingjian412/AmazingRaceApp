using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AmazingRaceApp.Startup))]
namespace AmazingRaceApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
