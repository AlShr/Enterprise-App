using System;
using Ninject;

namespace Enterprise.Data
{
    public sealed class DaoLocatorNinject : IDisposable
    {
        private static IKernel appKernel = new StandardKernel(new ContainerNinjectModule());

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
