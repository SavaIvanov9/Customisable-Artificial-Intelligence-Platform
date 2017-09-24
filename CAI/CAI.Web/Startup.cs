using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CAI.Web.Startup))]
namespace CAI.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
