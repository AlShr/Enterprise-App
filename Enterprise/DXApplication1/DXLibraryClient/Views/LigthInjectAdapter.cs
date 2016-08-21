using System;
using System.Linq.Expressions;
using LightInject;
using LibraryClient.Common;



namespace DXLibraryClient
{
    public class LigthInjectAdapter:IContainer
    {
        private readonly ServiceContainer container = new ServiceContainer();
        public void Register<TService, TImplementation>() where TImplementation : TService
        {
            container.Register<TService, TImplementation>();
        }

        public void Register<TService>()
        {
            container.Register<TService>();
        }

        public void RegisterInstance<T>(T instance)
        {
            container.RegisterInstance(instance);
        }

        public void Register<TService, TArgument>(Expression<Func<TArgument, TService>> factory)
        {
            container.Register(serviceFactory => factory);
        }

        public void Register<TService, TArgument1, TArgument2>(Expression<Func<TArgument1, TArgument2, TService>> factory)
        {
            container.Register(serviceFactory => factory);
        }

        public TService Resolve<TService>()
        {
            return container.GetInstance<TService>();
        }

        public bool IsRegistered<TService>()
        {
            return container.CanGetInstance(typeof(TService), string.Empty);
        }

    }
}
