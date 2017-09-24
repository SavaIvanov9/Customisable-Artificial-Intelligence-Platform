//[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(CAI.IoC.App_Start.NinjectWebCommon), "Start")]
//[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(CAI.IoC.App_Start.NinjectWebCommon), "Stop")]

namespace CAI.IoC.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Services;
    using Services.Abstraction;

    public static class WebContainer
    {
        //private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        ///// <summary>
        ///// Starts the application
        ///// </summary>
        //public static void Start() 
        //{
        //    DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
        //    DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
        //    bootstrapper.Initialize(CreateKernel);
        //}
        
        ///// <summary>
        ///// Stops the application.
        ///// </summary>
        //public static void Stop()
        //{
        //    bootstrapper.ShutDown();
        //}

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        public static IKernel GetNinjectKernel
        {
            get
            {
                IKernel kernel = null;

                if (kernel == null)
                {
                    kernel = new StandardKernel();
                    RegisterServices(kernel);
                }

                return kernel;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<ITestService>().To<TestService>();
        }        
    }
}
