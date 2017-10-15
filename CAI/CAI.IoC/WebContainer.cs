[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(CAI.IoC.WebContainer), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(CAI.IoC.WebContainer), "Stop")]

namespace CAI.IoC
{
    using Data;
    using Data.Abstraction;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject;
    using Ninject.Web.Common;
    using Services;
    using Services.Abstraction;
    using System;
    using System.Web;

    public static class WebContainer
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            //Data
            kernel.Bind<ICaiDbContext>().To<CaiDbContext>();
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>();

            //Services
            kernel.Bind<INeuralNetworkService>().To<NeuralNetworkService>();
            kernel.Bind<ILanguageProcessingService>().To<LanguageProcessingService>();
            kernel.Bind<IIntentionRecognitionService>().To<IntentionRecognitionService>();
            kernel.Bind<IBotService>().To<BotService>();
            kernel.Bind<IIntentionService>().To<IntentionService>();
            kernel.Bind<ISignInManagerService>().To<SignInManagerService>();
            kernel.Bind<IUserManagerService>().To<UserManagerService>();
            
        }
    }
}
