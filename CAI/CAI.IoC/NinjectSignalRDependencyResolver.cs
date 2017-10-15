namespace CAI.IoC
{
    using Microsoft.AspNet.SignalR;
    using Ninject;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class NinjectSignalRDependencyResolver : DefaultDependencyResolver
    {
        private readonly IKernel _kernel;
        public NinjectSignalRDependencyResolver(IKernel kernel)
        {
            this._kernel = kernel;
        }

        public override object GetService(Type serviceType)
        {
            return this._kernel.TryGet(serviceType) ?? base.GetService(serviceType);
        }

        public override IEnumerable<object> GetServices(Type serviceType)
        {
            return this._kernel.GetAll(serviceType).Concat(base.GetServices(serviceType));
        }
    }
}
