using LibraryClient.Views;

namespace LibraryClient.Common
{
    public interface IApplicationController
    {
        IApplicationController RegisterView<TView, TImplementation>()
            where TImplementation : class, TView
            where TView : IView;

        IApplicationController RegisterInstance<TArgument>(TArgument instance);

        IApplicationController RegisterService<TService, TImplementation>()
            where TImplementation : class, TService;

        void Run<TPresenter>()
            where TPresenter : class, IPresenter;

        void Run<TPresenter, TArgumnent>(TArgumnent argumnent)
            where TPresenter : class, IPresenter<TArgumnent>;

        void Run<TPresenter, TArgument1, TArgument2>(TArgument1 argument1, TArgument2 argument2)
            where TPresenter : class,IPresenter<TArgument1, TArgument2>;
    }
}
