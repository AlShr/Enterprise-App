using LibraryClient.Views;

namespace LibraryClient.Common
{
    public class ApplicationController : IApplicationController
    {
        private readonly IContainer container;

        public ApplicationController(IContainer container)
        {
            this.container = container;
            this.container.RegisterInstance<IApplicationController>(this);
        }

        public IApplicationController RegisterView<TView, TImplementation>()
            where TImplementation : class, TView
            where TView : IView
        {
            this.container.Register<TView, TImplementation>();
            return this;
        }

        public IApplicationController RegisterInstance<TInstance>(TInstance instance)
        {
            this.container.RegisterInstance(instance);
            return this;
        }

        public IApplicationController RegisterService<TModel, TImplementation>()
            where TImplementation : class, TModel
        {
            this.container.Register<TModel, TImplementation>();
            return this;
        }

        public void Run<TPresenter>() where TPresenter : class, IPresenter
        {
            if (!this.container.IsRegistered<TPresenter>())
                this.container.Register<TPresenter>();

            var presenter = this.container.Resolve<TPresenter>();
            presenter.Run();
        }

        public void Run<TPresenter, TArgumnent>(TArgumnent argumnent) where TPresenter : class, IPresenter<TArgumnent>
        {
            if (!this.container.IsRegistered<TPresenter>())
                this.container.Register<TPresenter>();

            var presenter = this.container.Resolve<TPresenter>();
            presenter.Run(argumnent);
        }

        public void Run<TPresenter, TArgument1, TArgument2>(TArgument1 argument1, TArgument2 argument2) where TPresenter : class
            ,IPresenter<TArgument1, TArgument2>
        {
            if (!this.container.IsRegistered<TPresenter>())
            {
                this.container.Register<TPresenter>();
            }
            var presenter = this.container.Resolve<TPresenter>();
            presenter.Run(argument1, argument2);
        }
    }
}
