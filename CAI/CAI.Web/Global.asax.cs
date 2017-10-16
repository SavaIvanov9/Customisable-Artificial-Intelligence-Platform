namespace CAI.Web
{
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());
        }
    }

    //public class MvcApplication : NinjectHttpApplication //: System.Web.HttpApplication 
    //{
    //    //protected void Application_Start()
    //    protected override void OnApplicationStarted()
    //    {
    //        AreaRegistration.RegisterAllAreas();
    //        FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
    //        RouteConfig.RegisterRoutes(RouteTable.Routes);
    //        BundleConfig.RegisterBundles(BundleTable.Bundles);
    //    }

    //    protected override IKernel CreateKernel()
    //    {
    //        return WebContainer.GetNinjectKernel;
    //    }
    //}
}