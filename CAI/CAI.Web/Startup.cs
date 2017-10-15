using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CAI.Web.Startup))]
namespace CAI.Web
{
    using IoC;
    using Microsoft.AspNet.SignalR;

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            var hubConfig = new HubConfiguration
            {
                Resolver = new NinjectSignalRDependencyResolver(WebContainer.Kernel)
            };

            app.MapSignalR(hubConfig);
        }
    }
}
