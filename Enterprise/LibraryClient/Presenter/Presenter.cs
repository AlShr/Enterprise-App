using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryClient.Presenter
{
    public class Presenter<TView> where TView : class,ISubView
    {
        public TView View { get; private set; }
        public Presenter(TView view)
        {
            if (view == null)
            {
                throw new ArgumentException("view");
            }
            View = view;
            View.Load += OnLoad;
        }

        protected virtual void OnLoad(object sender, EventArgs e)
        { }
    }
    public interface ISubView
    {
        event EventHandler Load;
    }
}
