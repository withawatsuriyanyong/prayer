using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Tuya01.Startup))]
namespace Tuya01
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
