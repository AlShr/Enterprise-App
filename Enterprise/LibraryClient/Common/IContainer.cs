using System;
using System.Linq.Expressions;

namespace LibraryClient.Common
{
    public interface IContainer
    {
        void Register<TService, TImplementation>() where TImplementation : TService;
        void Register<TService>();
        void RegisterInstance<T>(T instance);
        TService Resolve<TService>();
        bool IsRegistered<TService>();
        void Register<TService, TArgument>(Expression<Func<TArgument, TService>> factory);
        void Register<TService, TArgument1, TArgument2>(Expression<Func<TArgument1, TArgument2, TService>> factory);
    }
}
