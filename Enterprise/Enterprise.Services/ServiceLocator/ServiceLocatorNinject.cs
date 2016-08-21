using System;
using Ninject;

namespace Enterprise.Services.ServiceLocator
{
    public sealed class ServiceLocatorNinject:IDisposable
    {
        private static IKernel appKernel = new StandardKernel(new ServiceContainerNinjectModule());
        
        public static IKernel AppKernel
        {
            get { return appKernel; }
            internal set { appKernel = value; }
        }
        public void Dispose()
        {
            appKernel.Dispose();
        }
    }
}
