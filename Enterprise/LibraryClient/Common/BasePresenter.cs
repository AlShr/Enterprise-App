using System;
using LibraryClient.Views;

namespace LibraryClient.Common
{
    public abstract class BasePresenter<TView> : IPresenter where TView : IView
    {
        protected TView View { get; private set; }
         
        protected IApplicationController Controller { get; private set; }

        protected BasePresenter(IApplicationController controller, TView view)
        {
            Controller = controller;
            View = view;
            View.Load += OnLoad;
        }

        protected virtual void OnLoad(object sender, EventArgs e)
        { }

        public virtual void Run()
        {
            View.Show();
        }
    }
     
    public abstract class BasePresenter<TView, TArg> : IPresenter<TArg>
        where TView : IView
    {
        protected TView View { get; private set; }
        protected IApplicationController Controller { get; private set; }

        protected BasePresenter(IApplicationController controller, TView view)
        {
            Controller = controller;
            View = view;
        }

        public abstract void Run(TArg argument);
    }

    public abstract class BasePresenter<TView, TArg1, TArg2> : IPresenter<TArg1, TArg2>
        where TView : IView
    {
        protected TView View { get; private set; }
        protected IApplicationController Controller { get; private set; }
        protected BasePresenter(IApplicationController controller, TView view)
        {
            Controller = controller;
            View = view;
        }

        public abstract void Run(TArg1 argument1, TArg2 argument2);
    }
}
